using UnityEngine;

public enum ItemType
{
    None,
    Weapon,
    Shield,
    ChestArmor,
    Gloves,
    Boots,
    Accessory
}

public enum ItemGrade
{
    None,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public class    Item : MonoBehaviour
{
    private readonly int _id;
    private readonly string _name;
    private readonly int _atk;
    private readonly int _def;

    public int Id { get { return _id; } }

    public Item(int id, string name, int atk, int def)
    {
        _id = id;
        _name = name;
        _atk = atk;
        _def = def;
    }
}
