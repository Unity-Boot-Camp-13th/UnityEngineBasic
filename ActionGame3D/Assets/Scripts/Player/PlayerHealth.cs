using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100;       // 플레이어의 시작 체력
    public int currentHealth = 100;     // 플레이어의 현재 체력
    public Slider healthSlider;         // 체력 UI 와 연결
    public Image damageImage;           // 데미지 입을 경우에 대한 이미지
    public AudioClip deathClip;         // 플레이어 데미지 받을 때 쓸 오디오

    public float flashSpeed = 5.0f; // 화면 색이 변하고 다시 돌아가는 속도
    public Color flashColor = new Color(1, 0, 0, 0.1f); // 변경될 색

    Animator anim;                      // 애니메이터
    AudioSource playerAudio;            // 오디오소스
    PlayerMovement playerMovement;      // 플레이어 움직임
    bool isDead;                        // 죽음 확인용 변수
    bool isDamaged;                     // 데미지 확인용 변수

    private void Awake()
    {
        // 컴포넌트 연결
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        // 체력 설정
        currentHealth = startHealth;
    }

    private void Update()
    {
        if (isDamaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        isDamaged = false;
    }

    // 플레이어가 데미지를 받았을 때 호출할 함수
    public void TakeDamage(int amount)
    {
        isDamaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
        else
        {
            anim.SetTrigger("Damage");
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        playerMovement.enabled = false; //PlayerMovement�� ���� ��Ȱ��ȭ
    }
}