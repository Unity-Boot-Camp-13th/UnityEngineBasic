using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    Vector3 dir; // 움직일 방향
    public GameObject effect;

    private void Start()
    {
        int randValue = Random.Range(0, 10); // 0 이상 10 미만

        // 플레이어 방향으로 이동
        if (randValue < 3) // 0 1 2
        {
            // 게임 씬에서 "Player"를 검색합니다.
            var target = GameObject.Find("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize(); // 크기를 1로 맞춰, 방향 값만 구함
        }
        // 아래로 이동
        else 
        {
            dir = Vector3.down;
        }
    }


    private void Update()
    {
        // Vector3 dir = Vector3.down;

        // transform.Translate(dir * speed * Time.deltaTime);
        transform.position += dir * speed * Time.deltaTime;
    }

    // 충돌 시작
    private void OnCollisionEnter(Collision collision)
    {
        var explosion = Instantiate(effect);
        explosion.transform.position = transform.position;

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    // 충돌 끝
    private void OnCollisionExit(Collision collision)
    {
        
    }

    // 충돌 진행 중
    private void OnCollisionStay(Collision collision)
    {
        
    }
}
