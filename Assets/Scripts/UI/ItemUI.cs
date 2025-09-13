using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public sealed class ItemUIData
{
    public Sprite GradeSprite { get; }
    public Sprite IconSprite { get; }

    public ItemUIData(Sprite gradeSprite, Sprite iconSprite)
    {
        GradeSprite = gradeSprite;
        IconSprite = iconSprite;
    }
}

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image _grade;
    [SerializeField] private Image _icon;

    public void SetData(ItemUIData data)
    {
        if (data == null)
        {
            Debug.LogError("ItemUIData cannot be null.");
        }

        _grade.sprite = data.GradeSprite;
        _icon.sprite = data.IconSprite;
    }
}
