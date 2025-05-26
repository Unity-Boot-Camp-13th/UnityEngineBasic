using UnityEngine;

public class Player : MonoBehaviour
{
	public Inventory Inventory;
    public TileManager TileManager;

    private void Awake()
    {
        // 기본 인벤토리 8개 제공
        Inventory = new Inventory(8);
        TileManager = GameManager.Instance.TileManager;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // var position = new Vector3Int((int)transform.position.x,
            //                               (int)transform.position.y, 0);

            // 월드 좌표 설정
            var world = transform.position;

            // 월드 -> 셀
            var grid = TileManager.Interactables.WorldToCell(world);

            if (GameManager.Instance.TileManager.isInteractable(grid))
            {
                Debug.Log("Check");
                GameManager.Instance.TileManager.SetInteract(grid);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 채집 기능
        }
    }

    public void Drop(Item item)
    {
        // 생성 위치 설정
        var spawn = transform.position;

        // 던지는 범위
        float x = 1;

        Vector3 offset = new Vector3(x, 0, 0);

        // 드랍 오브젝트 생성
        var go = Instantiate(item, spawn + offset, Quaternion.identity);

        // 오브젝트에 대한 물리적인 힘 작용
        // go.rbody.AddForce(offset * 2f, ForceMode2D.Impulse);
    }
}