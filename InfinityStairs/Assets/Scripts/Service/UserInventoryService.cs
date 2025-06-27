using System.Collections.Generic;

public interface IUserInventoryService
{
    // 장착하지 않은 아이템 목록을 보여준다.
    List<ItemViewModel> UnequippedItems { get; }

    ItemViewModel GetItemViewModel(int id);
}

public class UserInventoryService : IUserInventoryService
{
    private IitemRepository _itemRepository;

    public UserInventoryService()
    {
        _itemRepository = new TestItemRepository();
    }

    public List<int> UnequippedItems;

    List<ItemViewModel> IUserInventoryService.UnequippedItems => throw new System.NotImplementedException();

    public ItemViewModel GetItemViewModel(int id)
    {
        Item item = _itemRepository.FindById(id);

        return new ItemViewModel(item.GradePath, item.IconPath);
    }
}
