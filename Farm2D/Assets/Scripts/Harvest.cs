using UnityEngine;

public enum CollectType
{
    None, Seed, Fruit, Crop, Bean, Egg, Milk
}

public class Harvest : MonoBehaviour
{
    public CollectType type;
    public Sprite icon;

    // private void Awake()
    // {
    //     icon = GetComponent<SpriteRenderer>();
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. 태그 조사
        if (collision.CompareTag("Player"))
        {
            // var player = collision.GetComponent<PlayerMovement>();
            // player.stat.count_of_harvest++;

            // 2. 플레이어 클래스 확인
            var player = collision.GetComponent<Player>();
            // 3. 플레이어가 가진 인벤토리에 추가
            player.Inventory.Add(this);

            Destroy(this.gameObject);
        }
    }
}
