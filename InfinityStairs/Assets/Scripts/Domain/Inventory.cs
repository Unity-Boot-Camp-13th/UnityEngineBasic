using System.Collections.Generic;
public class Inventory
{
    private List<UserInventoryItem> _items;

    // Inventory 외부에 아이템 목록을 전달하기 위한 프로퍼티
    public IReadOnlyList<UserInventoryItem> Items => _items.AsReadOnly();

}