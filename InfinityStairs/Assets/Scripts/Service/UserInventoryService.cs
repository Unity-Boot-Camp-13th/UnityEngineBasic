using System.Collections.Generic;
using System.Linq;
public interface IUserInventoryService
{
    // 장착하지 않은 아이템 목록을 보여준다.
    IReadOnlyList<UserInventoryItemModel> UnequippedItems { get; }
    ItemService ItemService { get; }

    void AcquireRandomItem();

    void AcquireItem(int itemId);
}

public class UserInventoryService : IUserInventoryService
{
    private IItemRepository _itemRepository;
    private readonly Inventory _inventory;
    private readonly ItemService _itemService;

    public ItemService ItemService {get {return _itemService;}}

    public UserInventoryService()
    {
        _itemRepository = new ItemRepository(new ResourcesJsonParser<ItemModelList>());
        _itemService = new ItemService(_itemRepository);
        _inventory = new Inventory();
    }

    public IReadOnlyList<int> AllItem => _itemRepository.FindAll().Select(item => item.Id).ToList();

    public IReadOnlyList<UserInventoryItemModel> UnequippedItems
    {
        get
        {
             return _inventory.Items
                .Select(item => new UserInventoryItemModel(item))
                .ToList();
        }
    }

    public void AcquireRandomItem()
    {
        // 1. 랜덤한 아이템 ID
        // int randomItemId = AllItem[Random.Range(0, AllItem.Count - 1)];
        int randomItemId = _itemService.GetRandomItemId();

        // 1. ID 규칙 찾기
        // 2. 모든 아이템 목록 불러와서 거기서 하나 찾자


        // 2. UserInventoryItem 으로 변환
        UserInventoryItem newItem = UserInventoryItem.Acquire(randomItemId);

        // 3. 인벤토리에 추가
        _inventory.AddItem(newItem);
    }

    public void AcquireItem(int itemId)
    {
        UserInventoryItem item = UserInventoryItem.Acquire(itemId);
    }
}
