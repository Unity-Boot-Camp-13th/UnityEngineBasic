// Item 객체의 영속성을 책임진다.
// CRUD (Create, Read, Update, Delete) 의 관점에서 생각해볼 때, 유저 입장에서는 Read 만 필요하다.
// ㄴ 유저가 직접 아이템을 만들고 업데이트하고 삭제할 일은 없다.

using System.Collections.Generic;
using System.Linq;

public interface IitemRepository
{
    Item FindById (int id);
}

public class TestItemRepository : IitemRepository
{
    private List<Item> _items;

    public TestItemRepository()
    {
        _items = new List<Item>()
        {
            new Item(11001),
            new Item(12001),
            new Item(13001),
            new Item(14001),
            new Item(15001),
            new Item(21001),
        };
    }

    public Item FindById(int id) => _items.Where(item => item.Id == id).First();
}