using UnityChan;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;                          // 컴포넌트
    Animator animator;

    private Vector3 moveVector;                              // 방향 백터
    [SerializeField] private float speed = 5.0f;             // 플레이어의 이동 속도
    [SerializeField] private float vertical_velocity = 0.0f; // 점프를 위한 수직 속도
    [SerializeField] private float gravity = 12.0f;          // 중력 값
    [SerializeField] private float jump = 8.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (Time.timeSinceLevelLoad < CameraController.camera_animate_duration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }


        moveVector = Vector3.zero; // 방향 백터 값 리셋

        if (controller.isGrounded)
        {
            vertical_velocity = 0.0f;

            // 점프 기능 추가
            if (Input.GetKey(KeyCode.Space))
            {
                vertical_velocity = jump;
                SetAnimator("IsJump");
            }
        }
        else
        {
            vertical_velocity -= gravity * Time.deltaTime;
        }

        // 1. 좌우 이동
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        // 2. 점프 관련
        moveVector.y = vertical_velocity;
        // 3. 앞으로 이동
        moveVector.z = speed;
        // 설정한 방향대로 이동 진행
        controller.Move(moveVector * Time.deltaTime);
    }

    void SetAnimator(string temp)
    {
        // 기본 애니메이터 초기화
        animator.SetBool("IsRun", false);
        animator.SetBool("IsJump", false);
        animator.SetBool("IsSlide", false);

        animator.SetBool(temp, true);
    }
}
