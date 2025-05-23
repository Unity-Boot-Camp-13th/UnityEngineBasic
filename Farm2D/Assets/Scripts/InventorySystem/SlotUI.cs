using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    public Image slot_icon;
    public TextMeshProUGUI slot_count_text;

    public void SetSlot(Slot slot)
    {
        if (slot != null)
        {
            slot_icon.sprite = slot.icon;
            slot_count_text.text = slot.count.ToString();
        }
    }

    // 빈 슬롯 설정
    public void SetEmpty()
    {
        slot_icon.sprite = null;
        slot_count_text.text = "";
    }
}