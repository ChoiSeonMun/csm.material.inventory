using Gpm.Ui;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public sealed class InventoryItemSlotData : InfiniteScrollData
{
    public ItemDisplayData DisplayData { get; }
    public UserInventoryItemModel UserInventoryItem { get; }

    public InventoryItemSlotData(ItemDisplayData displayData, UserInventoryItemModel userInventoryItem)
    {
        DisplayData = displayData;
        UserInventoryItem = userInventoryItem;
    }
}

public sealed class InventoryItemSlot : InfiniteScrollItem
{
    [Header("Service")]
    [SerializeField] private ItemServiceLocatorSO _itemServiceLocator;

    [Header("UI Elements")]
    [SerializeField] private ItemUI _itemUI;

    private InventoryItemSlotData _data;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        _data = scrollData as InventoryItemSlotData;

        _itemUI.SetData(new ItemUIData(
            gradeSprite: _data.DisplayData.GradeSprite,
            iconSprite: _data.DisplayData.IconSprite
        ));
    }


    public void OnClickSlot()
    {
        UIOpener.OpenEquipmentUI(
            displayData: _data.DisplayData,
            userInventoryItem: _data.UserInventoryItem,
            itemService: _itemServiceLocator.Service,
            isEquipped: false
        );
    }
}
