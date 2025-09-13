using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private UserInventoryServiceLocatorSO _userInventoryServiceLocator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.Open<InventoryUI>(PopupUIData.Empty);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _userInventoryServiceLocator.Service.AcquireRandomItem();
            UIManager.Instance.Get<InventoryUI>().Refresh();
        }
    }
}
