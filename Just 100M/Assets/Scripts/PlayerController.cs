using Mono.Cecil.Cil;
using UnityChan;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;                          // 컴포넌트
    Animator animator;

    private Vector3 moveVector;
    private bool hasStarted = false;

    // 방향 백터
    [SerializeField] private float speed = 5.0f;             // 플레이어의 이동 속도
    [SerializeField] private float vertical_velocity = 0.0f; // 점프를 위한 수직 속도
    [SerializeField] private float gravity = 12.0f;          // 중력 값
    [SerializeField] private float jump = 8.0f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        // 처음 진입했을 때 카메라 무빙
        if (Time.timeSinceLevelLoad < CameraController.camera_animate_duration)
        {
            return;
        }
        
        // GroundCheck
        bool grounded = controller.isGrounded;

        // 입력 읽기
        float h = Input.GetAxisRaw("Horizontal");
        bool wantRun = grounded && hasStarted;
        bool wantJump = !grounded;
        bool wantSlide = grounded && Input.GetKey(KeyCode.DownArrow);

        // 애니메이션 파라미터 세팅
        animator.SetBool("IsRun", wantRun);
        animator.SetBool("IsJump", wantJump);
        animator.SetBool("IsSlide", wantSlide);

        // 물리 이동
        if (grounded)
            vertical_velocity = 0;

        if (grounded &&
            Input.GetKey(KeyCode.Space))
        {
            vertical_velocity = jump;
        }
        else if (!grounded)
        {
            vertical_velocity -= gravity * Time.deltaTime;
        }

        if (!hasStarted && Mathf.Abs(h) > 0.1f) 
            hasStarted = true;

        // 이동 벡터 계산
        moveVector.x = h * speed;
        moveVector.y = vertical_velocity;
        moveVector.z = hasStarted ? speed : 0f;

        controller.Move(moveVector * Time.deltaTime);
    }
}
