using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public interface IItemService
{
    public int GetRandomItemId();
    public string GetGradeSpritePath(int itemId);
    public string GetIconSpritePath(int itemId);
}


public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly Random _random;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public int GetRandomItemId()
    {
        IReadOnlyList<Item> items = _itemRepository.FindAll();
        Item randomItem = items[_random.Next(items.Count)];
        return randomItem.Id;
    }

    private const string k_TexturePath = "Textures";
    public string GetGradeSpritePath(int itemId)
    {
        return Path.Combine(k_TexturePath, GetGrade(itemId).ToString());
    }

    public string GetIconSpritePath(int itemId)
    {
        StringBuilder sb = new StringBuilder(itemId.ToString());
        sb[1] = '1';

        return Path.Combine(k_TexturePath, sb.ToString());
    }

    public ItemType GetType(int id)
    {
        int typeDigit = id / 10000;

        return typeDigit switch
        {
            1 => ItemType.Weapon,
            2 => ItemType.Shield,
            3 => ItemType.ChestArmor,
            4 => ItemType.Gloves,
            5 => ItemType.Boots,
            6 => ItemType.Accessory,
            _ => throw new ArgumentException($"Invalid item id : {id}")
        };
    }

    public ItemGrade GetGrade(int id)
    {
        int gradeDigit = id / 1000 % 10;

        return gradeDigit switch
        {
            1 => ItemGrade.Common,
            2 => ItemGrade.Uncommon,
            3 => ItemGrade.Rare,
            4 => ItemGrade.Epic,
            5 => ItemGrade.Legendary,
            _ => throw new ArgumentException($"Invalid item id : {id}")
        };
    }
}