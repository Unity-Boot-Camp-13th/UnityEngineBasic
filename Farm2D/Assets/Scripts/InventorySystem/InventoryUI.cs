using UnityEngine;
using System.Collections.Generic;


public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI; // 인벤토리 창
    public Player player; // 플레이어 등록
    public List<SlotUI> slots = new ();

    private void Update()
    {
        // 버튼 누르면 인벤토리 켜는 기능
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOff();
        }

        SlotRenewal();
    }

    public void OnOff()
    {
        // 켜짐 여부에 따라 true 와 false 로 인벤토리를 키거나 끕니다.
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }
        else
        {
            inventoryUI.SetActive(true);
        }
    }

    // 슬롯에 대한 갱신
    private void SlotRenewal()
    {
        if (slots.Count == player.Inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.Inventory.slots[i].item_name != "")
                {
                    // 슬롯에 이미지와 개수 등을 갱신한다.
                    slots[i].SetSlot(player.Inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int slot_idx)
    {
        Item drop = GameManager.Instance.ItemManager.GetItem(player.Inventory.slots[slot_idx].item_name);
        
        if (drop != null)
        {
            player.Drop(drop);
            player.Inventory.Remove(slot_idx);
            SlotRenewal();
        }
    }
}
