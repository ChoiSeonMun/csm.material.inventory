using NUnit.Framework.Constraints;
using UnityEngine;

public class PopupUIData
{
    public static PopupUIData Empty { get; } = new PopupUIData();
}

public abstract class PopupUI : MonoBehaviour
{
    public virtual void Init()
    {
        var rectTransform = transform as RectTransform;
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.offsetMin = Vector3.zero;
        rectTransform.offsetMax = Vector3.zero;
    }

    public virtual void Open(PopupUIData data)
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        UIManager.Instance.Close(this);
    }
}
