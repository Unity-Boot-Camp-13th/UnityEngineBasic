// Item 객체의 영속성을 책임진다.
// CRUD (Create, Read, Update, Delete) 의 관점에서 생각해볼 때, 유저 입장에서는 Read 만 필요하다.
// ㄴ 유저가 직접 아이템을 만들고 업데이트하고 삭제할 일은 없다.

using System.Collections.Generic;
using System.Linq;
public interface IItemRepository
{
    Item FindById (int id);

    IReadOnlyList<Item> FindAll ();
}

public class ItemRepository : IItemRepository
{
    private readonly string Path = "Data/items";
    private List<Item> _items;

    public ItemRepository(IParser<ItemModelList> parser)
    {
        _items = parser.LoadFrom(Path).data.Select(model => ToItem(model)).ToList();
    }

    public IReadOnlyList<Item> FindAll()
    {
        return _items;
    }

    public Item FindById(int id)
    {
        return _items.Find(item => item.Id == id);
    }

    private Item ToItem(ItemModel model)
    {
        return new Item(model.item_id, model.item_name, model.attack_power, model.defense);
    }
}