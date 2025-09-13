using System;
using UnityEngine;

public static class UIOpener
{
    public static void OpenEquipmentUI(
        IItemService itemService,
        ItemDisplayData displayData,
        UserInventoryItemModel userInventoryItem,
        bool isEquipped,
        Action onProcessButtonClicked = null)
    {
        Status itemStat = itemService.GetStat(userInventoryItem.ItemId);

        var uiData = new EquipmentUIData(
            data: displayData,
            userInventoryItem: userInventoryItem,
            isEquipped: isEquipped,
            itemStat: itemStat,
            onProcessButtonClicked: onProcessButtonClicked
        );

        UIManager.Instance.Open<EquipmentUI>(uiData);
    }
}