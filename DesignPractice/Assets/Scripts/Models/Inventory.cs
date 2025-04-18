using System;
using System.Collections.Generic;
using System.Linq;

namespace DP.Models
{
    [Serializable]
    public class Inventory
    {
        public Inventory(int slotNum)
        {
            Slots = new List<InventorySlot>(slotNum);

            for (int i = 0; i < slotNum; i++)
                Slots[i] = InventorySlot.Empty;
        }

        public List<InventorySlot> Slots;
    }
}