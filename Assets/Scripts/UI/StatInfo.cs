using System;
using TMPro;
using UnityEngine;

public sealed class StatInfoData
{
    public int Amount { get; }

    public StatInfoData(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        }

        Amount = amount;
    }
}


public sealed class StatInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountText;

    public void SetData(StatInfoData data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        _amountText.text = $"+{data.Amount}";
    }
}
