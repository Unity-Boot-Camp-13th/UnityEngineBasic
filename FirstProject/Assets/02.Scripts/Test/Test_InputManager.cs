using UnityEngine;

namespace Tset
{
    public class Test_InputManager : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _speed = 2f;
        bool _isDragging;

        private void Update()
        {
           if (Input.GetKeyDown(KeyCode.Space))
           {
                Debug.Log("Space down");
           }
           else if (Input.GetKey(KeyCode.Space))
           {
                Debug.Log("Space press");
           }
           else if (Input.GetKeyUp(KeyCode.Space))
           {
                Debug.Log("Space Up");
           }

           // HandleTarget();
        }

        void HandleTarget()
        {
            float h = Input.GetAxisRaw("Horizontal"); // 수평축 입력 (왼쪽 오른쪽 방향키)
            float v = Input.GetAxisRaw("Vertical"); // 수직축 입력 (위쪽 아래쪽 방향키)
            Vector3 direction = new Vector3(h, 0f, v).normalized; // 단위 벡터 구하기
            Debug.Log($"Direction: {h} {v}");
            Vector3 velocity = direction * _speed; // 속도 구하기
            Vector3 deltaPosition = velocity * Time.deltaTime; // 현재 프레임 위치변화량 구하기
            _target.Translate(deltaPosition); // 위치 변화
        }

        /// <summary>
        /// Mouse 0 : 왼쪽
        /// Mouse 1 : 오른쪽
        /// Mouse 2 : 스크롤
        /// </summary>
        void MouseDrag()
        {
            if (_isDragging)
            {
                if (Input.GetMouseButton(0) == false)
                {
                    _isDragging = false;
                }
            }
            else
            {
                if (!Input.GetMouseButtonDown(0))
                {
                    _isDragging = true;
                }

            }

        }
    }
}
