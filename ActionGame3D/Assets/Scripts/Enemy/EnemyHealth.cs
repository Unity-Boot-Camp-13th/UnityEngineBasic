using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100; // 시작 체력
    public int currentHealth;        // 현재 체력

    public float flashSpeed = 5.0f;             // 색 변경 시간
    public Color flashColor = new Color(1, 0, 0, 0.1f);  // 빨간색
    public float sinkSpeed = 1.0f;              // 슬라임이 죽으면 아래로 가라앉을 속도

    bool isDead, isSinking, damaged; // 적 상태에 따른 bool 값

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (damaged)
        {
            // transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColor);

        }
        else
        {
            // transform.GetChild(0).GetComponent<Renderer>().
            // material.SetColor("_OutlineColor",
            // Color.Lerp(transform.GetChild(0).
            // GetComponent<Renderer>().
            // material.GetColor("_OutlineColor"), Color.black, flashSpeed * Time.deltaTime));
        }
        damaged = false;

        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        if (currentHealth <= 0 && isDead)
        {
            Death();
        }
    }

    // 슬라임이 공격을 받으면 ,플레이어로부터 튕겨나가는 효과 연출
    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushback)
    {
        yield return new WaitForSeconds(delay);

        try
        {
            // 데미지 함수 실행
            TakeDamage(damage);

            // 플레이어로부터 멀어질 방향 계산
            Vector3 diff = (playerPosition - transform.position).normalized;

            //AddForce(수치, 모드);를 통해 물리적인 힘을 가합니다.
            GetComponent<Rigidbody>().AddForce(diff * 50f * pushback, ForceMode.Impulse);
        }
        catch (MissingReferenceException e) // 객체 참조가 유효하지 않은 상황에 대한 안내문
        {
            Debug.Log(e.ToString()); // 오류 메세지를 디버그로 남긴다.
        }
    }


    public void Death()
    {
        isDead = true;
        // 죽었을 때, 맵을 뚫고 아래로 가라앉는 처리를 진행하기 위한 처리
        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
        StartSinking();
    }

    private void StartSinking()
    {
        // 네비메쉬 설정을 끄겠습니다.
        // 맵과 장애물 등을 판단하여, 프렐이어 추적등을 진행할 때 사용
        GetComponent<NavMeshAgent>().enabled = false;
        // 물리 연산에 대한 처리를 하지 않겠습니다.
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2.0f);
    }
}
