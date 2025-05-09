
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;                          // 컴포넌트
    Animator animator;

    private Vector3 moveVector;
    private bool isJumping = false;


    // 방향 백터
    [Header("Direction")]
    [SerializeField] float speed = 5.0f;             // 플레이어의 이동 속도
    [SerializeField] float vertical_velocity = 0.0f; // 점프를 위한 수직 속도
    [SerializeField] float gravity = 12.0f;          // 중력 값
    [SerializeField] float jump = 8.0f;

    [Header("Ground Check")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;



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

        // 자동 전진
        moveVector = Vector3.forward * speed;
        moveVector.y = vertical_velocity;

        // 달리기 설정
        if (transform.position.y < 0.002f &&
            transform.position.y > 0f)
        {
            isJumping = false;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsRun", true);
        }

        // 좌우 이동
        float horizontal = Input.GetAxis("Horizontal");
        moveVector += Vector3.right * horizontal * speed;

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space) &&
            !isJumping)
        {
            vertical_velocity = jump;
            isJumping = true;
            animator.SetBool("IsRun", false);
            animator.SetBool("IsJump", true);
        }

        // 중력 처리
        vertical_velocity -= gravity * Time.deltaTime;
        if (vertical_velocity < 0.0f)
        {
            vertical_velocity = 0f;
        }
             

        // 이동 처리
        controller.Move(moveVector * Time.deltaTime);
    }
}
