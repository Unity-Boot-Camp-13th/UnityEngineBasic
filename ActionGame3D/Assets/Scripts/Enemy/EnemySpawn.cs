using UnityEngine;
public class EnemySpawn : MonoBehaviour
{
    public PlayerHealth playerHealth; // 플레이어 체력 (죽음 상태 확인)
    public GameObject enemy; // 소환할 몬스터
    public float intervalTime = 10.0f; // 반복 시간 (젠타임)
    public Transform[] spawnPools; // 소환 지점

    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("Spawn", intervalTime, intervalTime);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0)
            return;

        Vector2 randomOffset = Random.insideUnitCircle * 3.0f;

        Vector3 spawnPos = new Vector3(player.position.x + randomOffset.x,
                           player.position.y,
                           player.position.z + randomOffset.y);

        Instantiate(enemy, spawnPos, Quaternion.identity);
    }
}