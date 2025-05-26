using System;
using UnityEngine;

[Serializable]
public class Slot
{
    public string item_name;
    public int count;
    public int max_count;
    public Sprite icon;
    public Slot()
    {
        item_name = "";
        count = 0;
        max_count = 10; // 본인이 기본으로 설정할 숫자로 등록

        // 레벨업 시스템이나 아이템 등을 통해, max_count 양을 늘리거나 (슬롯),
        // 슬롯 개수 자체를 늘리는 등을 고려해볼 수 있습니다. (인벤토리)
    }

    // 현재 아이템 개수가 max_count 보다 작을 경우에 추가를 요청
    public bool Addable()
    {
        return count < max_count ? true : false;
    }

    // 추가 가능 여부에 대한 함수
    public void Add(Item item) 
    {
        item_name = item.data.itemName;
        icon = item.data.icon;
        count++;
    }

    public void Remove()
    {
        if (count > 0)
        {
            count--;

            if (count == 0)
            {
                icon = null;
                item_name = "";
            }
        }
    }
}