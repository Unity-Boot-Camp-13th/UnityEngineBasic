using System;
using UnityEngine;
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

        [SerializeField] Vector3 _bottomCenter;
        Node[,] _nodes;
        [SerializeField] GameObject[] _basicBlocks;


        public void Awake()
        {
            _nodes = new Node[_sizeY, _sizeX];
        }

        private void Start()
        {
            SetNodesRandomly();
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
                    blockIndex = Random.Range(0, typeArray.Length - 1);
                    nodeType = (NodeTypes)(1 << blockIndex);
                    _nodes[i, j] = new Node(j, i, nodeType);
                    block = Instantiate(_basicBlocks[blockIndex]);

                    block.transform.position = new Vector3(x: -(_sizeX / 2) + (((j - 1) / 2) * _nodeWidth),
                                                           y: (i + 0.5f) * _nodeHeight,
                                                           z: 0f);
                }
            }
        }
    }
}