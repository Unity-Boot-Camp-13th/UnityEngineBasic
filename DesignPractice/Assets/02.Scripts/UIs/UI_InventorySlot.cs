using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DP.Models;

namespace DP.UIs
{
    public class UI_InventorySlot : MonoBehaviour
    {
        [SerializeField] Image _itemIcon;
        [SerializeField] TextMeshProUGUI _itemNum;


        public void Render(int itemId, int itemNum)
        {
            // TODO : itemId 로 itemIcon 에 쓸 이미지 검색하는 수단 구현
            _itemNum.text = itemNum.ToString();
        }
    }
}
