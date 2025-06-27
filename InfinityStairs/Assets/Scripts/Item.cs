using System;
using System.Security;
using System.Text;
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

public class Item : MonoBehaviour
{
    public string GradePath => $"{_grade}";
    public string IconPath
    {
        get
        {
            StringBuilder sb = new StringBuilder(_id.ToString());
            sb[1] = '1';

            return sb.ToString();
        }
    }

    public Item(int id)
    {
        _id = id;

        // 식별자로부터 type, grade 추출
        // int 로 사용할 거니까 중간에 변환이 필요없음. 효율적으로 좋지 않음.
        // StringBuilder sb = new StringBuilder(_id.ToString());
        // _type = (ItemType)(int)sb[0];
        // _grade = (ItemGrade)(int)sb[1];

        _type = GetType(_id);
        _grade = GetGrade(_id);
    }

    private static ItemType GetType (int id)
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

    private static ItemGrade GetGrade (int id)
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

    public int Id { get { return _id; } }

    private int _id;
    private ItemType _type;
    private ItemGrade _grade;
    private string _name;
    private int _atk;
    private int _def;
}
