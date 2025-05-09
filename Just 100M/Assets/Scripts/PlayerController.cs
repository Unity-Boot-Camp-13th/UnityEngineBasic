
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;                          // 컴포넌트
    Animator animator;

    private Vector3 moveVector;
    private bool isJumping = false;
    [HideInInspector] public bool isDead = false;
    CoinCollector coinCollector;


    // 방향 백터
    [Header("Direction")]
    [SerializeField] float speed = 5.0f;             // 플레이어의 이동 속도
    [SerializeField] float vertical_velocity = 0.0f; // 점프를 위한 수직 속도
    [SerializeField] float gravity = 12.0f;          // 중력 값
    [SerializeField] float jump = 8.0f;

    [Header("Ground Check")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    // 플레이어 동결용
    private bool isFrozen = false;
    private float freezeEndTime = 0f;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        coinCollector = GetComponent<CoinCollector>();
    }


    void Update()
    {
        // 처음 진입했을 때 카메라 무빙
        if (Time.timeSinceLevelLoad < CameraController.camera_animate_duration)
        {
            return;
        }

        if (isFrozen)
        {
            // real time 으로 6초가 지났으면 해동
            if (Time.realtimeSinceStartup >= freezeEndTime)
            {
                isFrozen = false;
                animator.SetBool("IsRun", true);
            }
            else
            {
                return; // 동결 해제 전까지 나머지 로직 무시
            }
        }

        // 자동 전진
        moveVector = Vector3.forward * speed;
        moveVector.y = vertical_velocity;

        // 달리기 설정
        if (transform.position.y < 0.002f &&
            transform.position.y > 0f &&
            !isFrozen)
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

        // 죽음 처리
        if (transform.position.y < -1f)
        {
            isDead = true;
        }

        // 몬스터 등장하면 잠시 멈춤
        if (coinCollector.monsterInstance != null)
        {
            Freeze(6f);
            coinCollector.monsterInstance = null;
        }
    }

    // <summary>
    /// 플레이어를 real 시간 기준으로 seconds 초 동안 멈춥니다.
    /// </summary>
    private void Freeze(float seconds)
    {
        isFrozen = true;
        freezeEndTime = Time.realtimeSinceStartup + seconds;
        animator.SetBool("IsRun", false);
    }
}
