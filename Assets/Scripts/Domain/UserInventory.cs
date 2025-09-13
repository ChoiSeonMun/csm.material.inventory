using System;
using System.Collections.Generic;
using System.Linq;

public sealed class UserInventory
{
    private readonly Inventory _inventory;
    private readonly Equipment _equipment;

    public static UserInventory Empty()
    {
        return new UserInventory(new Inventory(), new Equipment());
    }

    public UserInventory(Inventory inventory, Equipment equipment)
    {
        _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        _equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
    }

    public void AcquireItem(int itemId)
    {
        long serialNumber = DateTime.UtcNow.Ticks;
        UserInventoryItem newItem = new UserInventoryItem(serialNumber, itemId);
        _inventory.AddItem(newItem);
    }

    public void Equip(EquipSlotType equipSlotType, long serialNumber)
    {
        var item = _inventory.GetItem(serialNumber);
        _equipment.Equip(equipSlotType, item);
    }

    public void Unequip(EquipSlotType equipSlotType)
    {
        _equipment.Unequip(equipSlotType);
    }

    public IReadOnlyList<UserInventoryItem> UnequippedItems => _inventory.Items
        .Where(item => _equipment.IsEquipped(item) == false)
        .ToList();

    public IReadOnlyDictionary<EquipSlotType, UserInventoryItem> EquippedItems => _equipment.EquippedItems;

    public IReadOnlyList<UserInventoryItem> AllItems => _inventory.Items;
}