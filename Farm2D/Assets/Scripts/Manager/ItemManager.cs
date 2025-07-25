using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items;

    private Dictionary<string, Item> harvestMap = new();

    private void Awake()
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    private void Add(Item item)
    {
        if (!harvestMap.ContainsKey(item.data.itemName))
        {
            harvestMap.Add(item.data.itemName, item);
        }
    }

    public Item GetItem(string name)
    {
        if (harvestMap.ContainsKey(name))
        {
            return harvestMap[name];
        }
        return null;
    }
}