using Gpm.Ui;
using UnityEngine;
using UnityEngine.UI;

// View 에 사용하는 데이터 : ViewModel
public class InventoryItemSlotData : InfiniteScrollData
{
    public Sprite IconSprite { get; }
    public Sprite GradeSprite { get; }
    public InventoryItemSlotData(UserInventoryItemModel model, ItemService service)
    {
        // Advance: 매번 Resources Load 대신 캐싱
        IconSprite = Resources.Load<Sprite>(service.GetIconSpritePath(model.ItemId)); // 파일 시스템 접근
        GradeSprite = Resources.Load<Sprite>(service.GetGradeSpritePath(model.ItemId));
    }
}

public class InventoryItemSlot : InfiniteScrollItem
{
    [SerializeField] Image _icon;
    [SerializeField] Image _grade;
    private Text _text;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        InventoryItemSlotData data = scrollData as InventoryItemSlotData;

        _icon.sprite = data.IconSprite;
        _grade.sprite = data.GradeSprite;
    }
}