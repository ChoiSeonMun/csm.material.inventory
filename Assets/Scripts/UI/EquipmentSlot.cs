using UnityEngine;
using UnityEngine.UI;
using System;

public sealed class EquipmentSlotData
{
    public ItemDisplayData DisplayData { get; }
    public UserInventoryItemModel UserInventoryItem { get; }

    public EquipmentSlotData(ItemDisplayData displayData, UserInventoryItemModel userInventoryItem)
    {
        DisplayData = displayData;
        UserInventoryItem = userInventoryItem;
    }
}

public sealed class EquipmentSlot : MonoBehaviour
{
    [Header("Service")]
    [SerializeField] private ItemServiceLocatorSO _itemServiceLocator;

    [Header("UI Elements")]
    [SerializeField] private ItemUI ItemUI;

    private EquipmentSlotData _data;

    public void Equip(EquipmentSlotData data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        _data = data;

        ItemUI.SetData(new ItemUIData(data.DisplayData.GradeSprite, data.DisplayData.IconSprite));
        ItemUI.gameObject.SetActive(true);
    }

    public void Unequip()
    {
        ItemUI.gameObject.SetActive(false);
    }

    public void OnClickSlot()
    {
        UIOpener.OpenEquipmentUI(
            displayData: _data.DisplayData,
            userInventoryItem: _data.UserInventoryItem,
            itemService: _itemServiceLocator.Service,
            isEquipped: true,
            onProcessButtonClicked: () => Unequip()
        );
    }
}
