using System;
using System.Linq;
using System.IO;
using UnityEngine;

public interface IUserInventoryRepository
{
    UserInventory Load();
    void Save(UserInventory userInventory);
}

public sealed class UserInventoryRepository : IUserInventoryRepository
{
    private readonly string _filePath;

    public UserInventoryRepository(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    public UserInventory Load()
    {
        if (false == File.Exists(_filePath))
        {
            return UserInventory.Empty();
        }

        string jsonFile = File.ReadAllText(_filePath);
        var model = JsonUtility.FromJson<UserInventoryModel>(jsonFile);
        if (model == null)
        {
            throw new ArgumentException("Failed to parse UserInventoryModel from JSON file.", nameof(jsonFile));
        }

        var allItems = model.items
            .Select(item => new UserInventoryItem(item.serial_number, item.item_id))
            .ToList();
        var inventory = new Inventory(allItems);

        var equipment = new Equipment();
        foreach (var equipSlotModel in model.equip_slots)
        {
            var item = inventory.GetItem(equipSlotModel.serial_number);
            equipment.Equip(equipSlotModel.equip_slot_type, item);
        }

        return new UserInventory(inventory, equipment);
    }

    public void Save(UserInventory userInventory)
    {
        if (userInventory == null)
        {
            throw new ArgumentNullException(nameof(userInventory));
        }

        var model = new UserInventoryModel
        {
            items = userInventory.AllItems
                .Select(item => new UserInventoryItemData
                {
                    serial_number = item.SerialNumber,
                    item_id = item.ItemId
                })
                .ToArray(),
            equip_slots = userInventory.EquippedItems
                .Select(kvp => new EquipSlotData
                {
                    equip_slot_type = kvp.Key,
                    serial_number = kvp.Value.SerialNumber
                })
                .ToArray()
        };

        string json = JsonUtility.ToJson(model, true);
        File.WriteAllText(_filePath, json);
    }
}