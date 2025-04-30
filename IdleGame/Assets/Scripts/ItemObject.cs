using System.Collections;
using TMPro;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Transform ItemText;
    public TextMeshProUGUI text; // TMP 쓰시는 분들은 TMP 로 설정

    public float angle = 45.0f;
    public float gravity = 9.8f;
    public float range = 2.0f;

    bool ischeck = false;

    // 아이템 레어도별로 처리하는 코드
    void ItemRare()
    {
        if (ischeck) return;
        ischeck = true;
        transform.rotation = Quaternion.identity; // 그대로 값 넘어가도록 회전 값 0
        // 아이템 텍스트 활성화
        ItemText.gameObject.SetActive(true);
        ItemText.parent = B_Canvas.instance.GetLayer(2);
        text.text = "Item"; // 아이템 이름 설정
    }

    private void Update()
    {
        if (ischeck == false)
            return;
        
        ItemText.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void Init(Vector3 pos)
    {
        // 풀링 재사용 시 이전 상태 초기화
        ischeck = false;
        ItemText.gameObject.SetActive(false);



        // 전달받은 값을 기준으로 그 주변에 위치할 수 있도록 범위 설정
        Vector3 item_pos = new Vector3
                           (pos.x + (Random.insideUnitSphere.x * range), 
                            0.0f,
                            pos.z + (Random.insideUnitSphere.z * range));
        // 이 기능을 몬스터 쪽의 사망 시 판정에서 작업 진행
        
        // 물체 이동 시작
        StartCoroutine(Simulate(item_pos));
    }


    IEnumerator Simulate(Vector3 pos)
    {
        float target_Distance = Vector3.Distance(transform.position, pos);
        float radian = angle * Mathf.Deg2Rad; // 라디안 변환 값
        float velocity = Mathf.Sqrt((target_Distance) * gravity / Mathf.Sin(2 * radian));

        float vx = velocity * Mathf.Cos(radian);
        float vy = velocity * Mathf.Sin(radian);

        float duration = target_Distance / vx;

        transform.rotation = Quaternion.LookRotation(pos - transform.position);
        // LookAt 처럼 회전 방향 바라보게 하는 코드

        float simulate_time = 0.0f;

        while (simulate_time < duration)
        {
            simulate_time += Time.deltaTime;

            // 시간이 지날 수록 위에서 점점 아래로 밑변 방향으로 이동
            transform.Translate(0, (vy - (gravity * simulate_time)) * Time.deltaTime, vx * Time.deltaTime);
            yield return null;
        }
        // 아이템 이동 시뮬레이션 끝나면 레어도 체크 후 화면에 아이템 이름 띄우기
        ItemRare();
    }
}