using UnityEngine;
using TMPro;  // TextMeshPro 사용 시

[RequireComponent(typeof(AudioSource))]
public class CoinCollector : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Sound")]
    [SerializeField] AudioClip collectSfx;
    [SerializeField] AudioClip hitObstacle;
    [SerializeField] AudioClip pingClip;

    public GameObject Monster;

    [HideInInspector] public int score = 0;
    AudioSource audioSource;

    [HideInInspector]
    public GameObject monsterInstance;  // 실제 스폰된 몬스터를 저장

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateScoreUI();
    }

    // CharacterController가 Trigger를 검사해 줌
    void OnTriggerEnter(Collider other)
    {
        // coin 먹을 때
        if (other.CompareTag("Coin"))
        {
            // 1) 점수 증가
            score++;

            // 2) UI 갱신
            UpdateScoreUI();

            // 3) 사운드 재생
            if (collectSfx != null)
                audioSource.PlayOneShot(collectSfx);

            // 4) 코인 제거
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Box"))
        {
            Vector3 spawnPosition = other.transform.position;
            spawnPosition.z += 5;

            monsterInstance = Instantiate(Monster, spawnPosition, Quaternion.Euler(0f, 180f, 0f));
            
            audioSource.PlayOneShot(pingClip);

            Destroy(other.gameObject);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider collider = hit.collider;

        if (hit.collider.CompareTag("Obstacle"))
        {
            score -= 5;
            UpdateScoreUI();

            if (hitObstacle != null)
                audioSource.PlayOneShot(hitObstacle);

            collider.tag = "Untagged";
        }
    }

    private void Update()
    {
        if (score < 0)
        {
            score = 0;
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Luck Score: {score}";
    }
}