// 인벤토리 데이터를 user에게 보여줄 목적

using DP.Models;
using DP.Repositories;
using DP.UIs;
using UnityEngine;
using System.Collections.Generic;

namespace DP.Views
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] UI_InventorySlot _slotUIPrefab;
        [SerializeField] RectTransform _slotUIContent;
        List<UI_InventorySlot> _slotUIs;


        private void Start()
        {
            SpawnSlotUIs();
        }

        private void OnEnable()
        {
            InventoryRepository.Singleton.OnInventoryChanged += Render;
        }


        private void OnDisable()
        {
            InventoryRepository.Singleton.OnInventoryChanged -= Render;
        }

        public void Render(IEnumerable<InventorySlot> slots)
        {
            int slotIndex = 0;

            // 인벤토리 슬롯 수가 런타임 중 증가할 상황이 있을 때 가정 :
            using (IEnumerator<InventorySlot> slotEnum = InventoryRepository.Singleton.GetAllSlots().GetEnumerator())
            using (IEnumerator<UI_InventorySlot> slotUIEnum = _slotUIs.GetEnumerator())
            {
                // 슬롯 데이터가 있음
                while (slotEnum.MoveNext())
                {
                    // 슬롯 UI 가 있음
                    if (slotUIEnum.MoveNext())
                    {
                        // 슬롯 데이터도 있고 만들어놓은 UI 도 있으니 UI 에 데이터 그리겠다.
                        slotUIEnum.Current.Render(slotEnum.Current.ItemId, slotEnum.Current.ItemNum);
                    }
                    // 슬롯 UI 가 없으니 새로 만들어서 데이터 그림
                    else
                    {
                        UI_InventorySlot slotUI = Instantiate(_slotUIPrefab, _slotUIContent);
                        slotUI.index = slotIndex;
                        slotUI.Render(slotEnum.Current.ItemId, slotEnum.Current.ItemNum);
                        _slotUIs.Add(slotUI);
                    }

                    slotIndex++;
                }
            }
        }

        private void SpawnSlotUIs()
        {
            _slotUIs = new List<UI_InventorySlot>(); // TODO : Reserving
            int slotIndex = 0;

            // user 한테 보여줘야 하는 데이터를 전부 순회
            foreach (InventorySlot slotData in InventoryRepository.Singleton.GetAllSlots())
            {
                UI_InventorySlot slotUI = Instantiate(_slotUIPrefab, _slotUIContent); // 슬롯 데이터를 보여줄 게임 오브젝트 생성
                slotUI.index = slotIndex;
                slotUI.Render(slotData.ItemId, slotData.ItemNum); // 생성한 게임 오브젝트 슬롯 데이터로 그릴 내용 갱신
                _slotUIs.Add(slotUI);
                slotIndex++;
            }
        }
    }
}
