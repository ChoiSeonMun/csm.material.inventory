using System.Collections.Generic;
using System.Linq;
using System;

public interface IUserInventoryService
{
    IReadOnlyDictionary<EquipSlotType, UserInventoryItemModel> EquippedItems { get; }
    IReadOnlyList<UserInventoryItemModel> UnequippedItems { get; }

    void AcquireRandomItem();
    void AcquireItem(int itemId);
    void Equip(UserInventoryItemModel itemModel);
    void Unequip(UserInventoryItemModel itemModel);

    Status GetTotalEquipmentStat();
}

public sealed class UserInventoryService : IUserInventoryService
{
    private readonly IUserInventoryRepository _userInventoryRepository;
    private readonly IItemService _itemService;
    private readonly UserInventory _userInventory;

    public UserInventoryService(IUserInventoryRepository userInventoryRepository, IItemService itemService)
    {
        _userInventoryRepository = userInventoryRepository ?? throw new ArgumentNullException(nameof(userInventoryRepository));
        _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));

        _userInventory = _userInventoryRepository.Load();
    }

    public IReadOnlyList<UserInventoryItemModel> UnequippedItems => _userInventory.UnequippedItems
        .Select(item => new UserInventoryItemModel(item))
        .ToList();

    public IReadOnlyDictionary<EquipSlotType, UserInventoryItemModel> EquippedItems => _userInventory.EquippedItems
        .ToDictionary(kvp => kvp.Key, kvp => new UserInventoryItemModel(kvp.Value));

    public void AcquireItem(int itemId)
    {
        _userInventory.AcquireItem(itemId);
        _userInventoryRepository.Save(_userInventory);
    }

    public void AcquireRandomItem()
    {
        int randomItemId = _itemService.GetRandomItemId();
        AcquireItem(randomItemId);
    }

    public void Equip(UserInventoryItemModel itemModel)
    {
        var equipSlotType = _itemService.GetEquipSlotType(itemModel.ItemId);
        _userInventory.Equip(equipSlotType, itemModel.SerialNumber);
        _userInventoryRepository.Save(_userInventory);
    }

    public Status GetTotalEquipmentStat()
    {
        Status totalStat = Status.Zero;

        var equippedItems = _userInventory.EquippedItems;
        foreach (UserInventoryItem equippedItem in equippedItems.Values)
        {
            Status itemStatus = _itemService.GetStat(equippedItem.ItemId);
            totalStat = totalStat.Add(itemStatus);
        }

        return totalStat;
    }

    public void Unequip(UserInventoryItemModel itemModel)
    {
        var equipSlotType = _itemService.GetEquipSlotType(itemModel.ItemId);
        _userInventory.Unequip(equipSlotType);
        _userInventoryRepository.Save(_userInventory);
    }
}
