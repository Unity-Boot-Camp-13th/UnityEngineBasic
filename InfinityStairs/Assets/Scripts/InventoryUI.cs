using System.Collections.Generic;
using Gpm.Ui;
using UnityEngine;



public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InfiniteScroll _scroll;
    [SerializeField] private UserInventoryServiceLocaterSO _userInventoryServiceLocater;

    
    private void Start()
    {
        IReadOnlyList<UserInventoryItemModel> models = _userInventoryServiceLocater.Service.UnequippedItems;

        foreach (UserInventoryItemModel model in models)
        {
            _scroll.InsertData(new InventoryItemSlotData(model, _userInventoryServiceLocater.Service.ItemService));
        }
    }
}
