using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using Random = UnityEngine.Random;

namespace Match3.InGame.LevelSystems
{
    public class Map : MonoBehaviour
    {
        [Header("Map Size")]
        [SerializeField] int _sizeX = 8;
        [SerializeField] int _sizeY = 12;

        [Header("Node Size")]
        [SerializeField] float _nodeWidth = 1;
        [SerializeField] float _nodeHeight = 1;

        [Header("기타")]
        [SerializeField] Vector3 _center;

        Node[,] _nodes;
        [SerializeField] GameObject[] _basicBlocks;
        [SerializeField] Camera _camera;

        Vector3 _leftBottom;
        Vector3 _rightTop;
        float _boundLeft;
        float _boundRight;
        float _boundTop;
        float _boundBottom;

        [Header("Vibration")]
        [SerializeField] Vector3 _oscillationAmplitude; // 진폭
        [SerializeField] Vector3 _oscillationVelocity;
        List<(int, int)> _selectedIndices;


        public event Action<int, int, float, float, Vector3> OnMapCreated;


        public void Awake()
        {
            _nodes = new Node[_sizeY, _sizeX];
            _selectedIndices = new List<(int, int)>(_sizeX * _sizeY);

            _leftBottom = new Vector3(x: (0 - (_sizeX - 1) * 0.5f) * _nodeWidth,
                                      y: (0 + 0.5f) * _nodeHeight - _sizeY * _nodeHeight * 0.5f,
                                      z: 0f)
                                      + _center;
            _rightTop = new Vector3(x: ((_sizeX - 1) - (_sizeX - 1) * 0.5f) * _nodeWidth,
                                    y: ((_sizeY - 1) + 0.5f) * _nodeHeight - _sizeY * _nodeHeight * 0.5f,
                                    z: 0f)
                                    + _center;

            _boundLeft = _leftBottom.x - _nodeWidth * 0.5f;
            _boundBottom = _leftBottom.y - _nodeHeight * 0.5f;
            _boundRight = _rightTop.x + _nodeWidth * 0.5f;
            _boundTop = _rightTop.y + _nodeHeight * 0.5f;
        }

        private void Start()
        {
            SetNodesRandomly();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition); // 스크린에 터치한 위치에서 쏘는 선
                Plane plane = new Plane(Vector3.forward, _center); // 퍼즐맵이 존재하는 가상의 평면 데이터

                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 position = ray.GetPoint(distance);

                    (int x, int y) index = GetIndexFromPosition(position);

                    if (index.x >= 0 &&
                        index.y >= 0)
                    {
                        Debug.Log($"Selected : {index.x}, {index.y}");
                        SelectIndex(index.x, index.y);
                    }
                }
            }

            OscillateSelectedBlocks();
        }

        (int x, int y) GetIndexFromPosition(Vector3 position) // 튜플 ->  두 개 이상의 값을 하나로 전달할 때 사용
        {
            if (position.x < _boundLeft || position.x > _boundRight ||
                position.y < _boundBottom || position.y > _boundTop)
            {
                return (-1, -1);
            }

            int x = Mathf.RoundToInt(((position.x - _leftBottom.x) / _nodeWidth));
            int y = Mathf.RoundToInt(((position.y - _leftBottom.y) / _nodeHeight));

            return (x, y);


            // 밑의 두 줄이 같은 내용
            // ValueTuple<int, int> tuple = new ValueTuple<int, int>(3, 5);
            // (int, int) tuple2 = (3, 5);
        }


        public void SetNodesRandomly()
        {
            Array typeArray = Enum.GetValues(typeof(NodeTypes));
            int blockIndex = Random.Range(0, typeArray.Length - 1);
            NodeTypes nodeType = (NodeTypes)(1 << blockIndex);
            int totalNodeType = typeArray.Length - 1;
            GameObject block;

            for (int i = 0; i < _nodes.GetLength(0); i++)
            {
                for (int j = 0; j < _nodes.GetLength(1); j++)
                {
                    blockIndex = Random.Range(0, totalNodeType);
                    block = Instantiate(_basicBlocks[blockIndex]);
                    nodeType = (NodeTypes)(1 << blockIndex);

                    block.transform.position
                        = new Vector3(x: (j - (_sizeX - 1) * 0.5f) * _nodeWidth,
                                      y: (i + 0.5f) * _nodeHeight - _sizeY * _nodeHeight * 0.5f,
                                      z: 0f)
                        + _center;

                    _nodes[i, j] = new Node(j, i, nodeType, block.transform);
                }
            }

            OnMapCreated?.Invoke(_sizeX, _sizeY, _nodeWidth, _nodeHeight, _center);
            // OnMapCreated(); // 위와 동일
            // 대리자 호출 시 () 로 직접호출보다 Invoke를 쓰는 이유는
            // 1. 제 3자가 이 변수의 정의를 파킹하지 않아도 대리자라는 것을 알 수 있다.
            // 2. 필요시 ? Null Check 연산자 사용 편의도 있다.
        }

        void SelectIndex(int x, int y)
        {
            _selectedIndices.Add((x, y));
        }

        void DeselectIndex(int x, int y)
        {
            if (_selectedIndices.Remove((x, y)))
            {
                _nodes[y, x].Block.rotation = Quaternion.identity;
            }
        }

        void OscillateSelectedBlocks()
        {
            float angleX = Mathf.Sin(Time.time * _oscillationVelocity.x) * _oscillationAmplitude.x;
            float angleY = Mathf.Sin(Time.time * _oscillationVelocity.y) * _oscillationAmplitude.y;
            float angleZ = Mathf.Sin(Time.time * _oscillationVelocity.z) * _oscillationAmplitude.z;
            Transform block;

            foreach ((int x, int y) index in _selectedIndices)
            {
                block = _nodes[index.y, index.x].Block;
                block.rotation = Quaternion.Euler(angleX, angleY, angleZ);
            }

            bool CheckMatch(int x, int y, List<(int x, int y)> appendedResults)
            {
                bool result = false;
                // 현재 노드 타입
                NodeTypes currentType = _nodes[y, x].TypeFlags;

                List<(int x, int y)> tempTrackinglist = new List<(int x, int y)>(Mathf.Max(_sizeX, _sizeY));
                int i = y;
                int j = x;

                // 위아래 3개 이상 매치 탐색

                // 위쪽 탐색
                i = y + 1; // 한 칸 위

                while (i < _sizeY) // 위쪽 맵 경계
                {
                    // 한 칸 위의 블록이 같은 타입이면
                    if (_nodes[i, j].TypeFlags == currentType)
                    {
                        tempTrackinglist.Add((j, i)); // 추적대상등록
                    }
                    else
                    {
                        break; // 다른 색 나오면 위쪽 탐색 끝
                    }

                    i++; // 한 칸 위로
                }

                // 아래쪽 탐색
                i = y - 1; // 한 칸 아래로

                while (i >= 0) // 아래쪽 맵 경계
                {
                    // 한 칸 위의 블록이 같은 타입이면
                    if (_nodes[i, j].TypeFlags == currentType)
                    {
                        tempTrackinglist.Add((j, i)); // 추적대상등록
                    }
                    else
                    {
                        break; // 다른 색 나오면 아래쪽 탐색 끝
                    }

                    i--; // 한 칸 아래로
                }

                if (tempTrackinglist.Count >= 2)
                {
                    appendedResults.AddRange(tempTrackinglist); // 매치 조건된 블록들을 결과에 붙임
                    result = true;
                }

                // TODO : 좌우 똑같이 해야 함
                tempTrackinglist.Clear();

                // 오른쪽 탐색
                j = x + 1;
                while (j < _sizeX) // 오른쪽 맵 경계
                {
                    // 한 칸 위의 블록이 같은 타입이면
                    if (_nodes[i, j].TypeFlags == currentType)
                    {
                        tempTrackinglist.Add((j, i)); // 추적대상등록
                    }
                    else
                    {
                        break; // 다른 색 나오면 오른쪽 탐색 끝
                    }

                    j++; // 한 칸 오른쪽으로
                }

                // 왼쪽 탐색
                j = x - 1; // 한 칸 왼쪽으로

                while (j >= 0) // 왼쪽 맵 경계
                {
                    // 한 칸 위의 블록이 같은 타입이면
                    if (_nodes[i, j].TypeFlags == currentType)
                    {
                        tempTrackinglist.Add((j, i)); // 추적대상등록
                    }
                    else
                    {
                        break; // 다른 색 나오면 왼쪽 탐색 끝
                    }

                    j--; // 한 칸 아래로
                }

                if (tempTrackinglist.Count >= 2)
                {
                    appendedResults.AddRange(tempTrackinglist); // 매치 조건된 블록들을 결과에 붙임
                    result = true;
                }

                // 매치 조건이 하나라도 있다면 현재 위치도 결과에 추가해야된다 (현재 위치도 파괴해야하니까)
                if (result)
                {
                    appendedResults.Add((x, y));
                }

                return result;
            }
        }
    }
}