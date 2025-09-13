using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class EquipmentUIData : PopupUIData
{
    public ItemDisplayData DisplayData { get; }
    public UserInventoryItemModel UserInventoryItem { get; }
    public bool IsEquipped { get; }
    public Status ItemStat { get; }
    public Action OnProcessButtonClicked { get; }

    public EquipmentUIData(ItemDisplayData data, UserInventoryItemModel userInventoryItem, bool isEquipped, Status itemStat, Action onProcessButtonClicked = null)
    {
        DisplayData = data ?? throw new ArgumentNullException(nameof(data));
        UserInventoryItem = userInventoryItem ?? throw new ArgumentNullException(nameof(userInventoryItem));
        IsEquipped = isEquipped;
        ItemStat = itemStat;
        OnProcessButtonClicked = onProcessButtonClicked;
    }
}

public sealed class EquipmentUI : PopupUI
{
    [Header("Service Locator")]
    [SerializeField] private UserInventoryServiceLocatorSO _userInventoryServiceLocator;

    [Header("UI Elements")]
    [SerializeField] private Image _gradeImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _gradeText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _processButtonLabel;
    [SerializeField] private StatInfo _atkStatInfo;
    [SerializeField] private StatInfo _defStatInfo;

    private EquipmentUIData _data;

    public override void Open(PopupUIData data)
    {
        base.Open(data);

        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        _data = data as EquipmentUIData;
        
        _gradeImage.sprite = _data.DisplayData.GradeSprite;
        _iconImage.sprite = _data.DisplayData.IconSprite;
        _gradeText.text = GetGradeText(_data.DisplayData.Grade);
        _nameText.text = _data.DisplayData.Name;
        _atkStatInfo.SetData(new StatInfoData(_data.ItemStat.Atk));
        _defStatInfo.SetData(new StatInfoData(_data.ItemStat.Def));

        if (_data.IsEquipped)
        {
            _processButtonLabel.text = "Unequip";
        }
        else
        {
            _processButtonLabel.text = "Equip";
        }

        static string GetGradeText(ItemGrade grade)
        {
            return grade switch
            {
                ItemGrade.Common => "<color=blue>Common</color>",
                ItemGrade.Uncommon => "<color=green>Uncommon</color>",
                ItemGrade.Rare => "<color=purple>Rare</color>",
                ItemGrade.Epic => "<color=orange>Epic</color>",
                ItemGrade.Legendary => "<color=red>Legendary</color>",
                _ => throw new ArgumentOutOfRangeException(nameof(grade), grade, "Invalid item grade")
            };
        }
    }


    public void OnClickProcessButton()
    {
        if (_data.IsEquipped)
        {
            _userInventoryServiceLocator.Service.Unequip(_data.UserInventoryItem);
        }
        else
        {
            _userInventoryServiceLocator.Service.Equip(_data.UserInventoryItem);
        }

        _data.OnProcessButtonClicked?.Invoke();
        UIManager.Instance.Get<InventoryUI>().Refresh();

        Close();
    }
}
