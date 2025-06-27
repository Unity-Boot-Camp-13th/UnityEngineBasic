using System.IO;
using Gpm.Ui;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : InfiniteScrollItem
{
    [SerializeField] Image _icon;
    [SerializeField] Image _grade;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        InventoryItemSlotData data = scrollData as InventoryItemSlotData;

        const string k_TexturePath = "Textures";

        _icon.sprite = Resources.Load<Sprite>(Path.Combine(k_TexturePath, data.IconPath));
        _grade.sprite = Resources.Load<Sprite>(Path.Combine(k_TexturePath, data.GradePath));

    }
}