using UnityEngine;

public class Player : MonoBehaviour
{
	public Inventory Inventory;

    private void Awake()
    {
        // 기본 인벤토리 4개 제공
        Inventory = new Inventory(8);
    }
}