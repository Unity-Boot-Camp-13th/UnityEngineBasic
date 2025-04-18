using System;

namespace DP.Models
{
    /// <summary>
    /// InventorySlot 모델
    /// </summary>
    [Serializable] // 사용자정의자료형은 Serializable Attribute 를 추가해서 직렬화가 가능한 자료형이라는 것을 명시
    public struct InventorySlot
    {
        public InventorySlot(int itemId, int itemNum)
        {
            ItemId = itemId;
            ItemNum = itemNum;
        }


        public static InventorySlot Empty => new InventorySlot(0, 0);


        public int ItemId;
        public int ItemNum;


        public static InventorySlot operator +(InventorySlot slot, int num)
        {
            if (slot.ItemId <= 0)
                throw new Exception("아이템이 없는데 개수를 늘릴 수 없음");

            return new InventorySlot(slot.ItemId, slot.ItemNum + num);
        }

        public static InventorySlot operator -(InventorySlot slot, int num)
        {
            if (slot.ItemId <= 0)
                throw new Exception("아이템이 없는데 개수를 뺄 수는 없음");

            if (slot.ItemNum - num < 0)
                throw new Exception("슬롯 데이터는 개수를 음수로 가질 수 없음");

            if (slot.ItemId - num == 0)
                return InventorySlot.Empty;

            return new InventorySlot(slot.ItemId, slot.ItemNum - num);
        }



        public static bool operator ==(InventorySlot slot1, InventorySlot slot2)
            => (slot1.ItemId == slot2.ItemId) && (slot1.ItemNum == slot2.ItemNum);

        public static bool operator !=(InventorySlot slot1, InventorySlot slot2)
            => !(slot1 == slot2);
    }
}