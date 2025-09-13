using JetBrains.Annotations;
using System;

[Serializable]
public sealed class EquipSlotData
{
    public EquipSlotType equip_slot_type;
    public long serial_number;
}

[Serializable]
public sealed class UserInventoryItemData
{
    public long serial_number;
    public int item_id;
}

[Serializable]
public sealed class UserInventoryModel
{
    public UserInventoryItemData[] items;
    public EquipSlotData[] equip_slots;
}