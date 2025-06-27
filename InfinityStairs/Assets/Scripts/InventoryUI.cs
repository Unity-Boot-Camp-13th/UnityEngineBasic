using System.Collections.Generic;
using Gpm.Ui;
using UnityEngine;

public class InventoryItemSlotData : InfiniteScrollData
{
    public string IconPath { get; }
    public string GradePath { get; }
    public InventoryItemSlotData(string iconPath, string gradePath)
    {
        IconPath = iconPath;
        GradePath = gradePath;
    }
}

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InfiniteScroll _scroll;
    private IUserInventoryService _userInventoryService;

    private void Start()
    {
        // List<ItemViewModel> unequippedItems = _userInventoryService.UnequippedItems;
        // foreach (ItemViewModel item in unequippedItems)
        // {
        //     _scroll.InsertData(new InventoryItemSlotData(item.IconPath, item.GradePath));
        // }

         _userInventoryService = new UserInventoryService();

        List<int> wantedItemIds = new List<int>()
        {
            11001,
            12001,
            13001,
            14001,
            15001,
            21001,
        };

        foreach (int itemId in wantedItemIds)
        {
            ItemViewModel model = _userInventoryService.GetItemViewModel(itemId);
            _scroll.InsertData(new InventoryItemSlotData(model.IconPath, model.GradePath));
        }
    }


}
