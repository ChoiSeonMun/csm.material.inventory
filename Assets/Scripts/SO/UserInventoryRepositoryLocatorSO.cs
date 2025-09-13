using UnityEngine;

[CreateAssetMenu(fileName = "UserInventoryRepositorySO", menuName = "Scriptable Objects/UserInventoryRepositorySO")]
public sealed class UserInventoryRepositoryLocatorSO : ScriptableObject
{
    public IUserInventoryRepository Repository { get; private set; }

    public void Bootstrap(string filePath)
    {
        Repository = new UserInventoryRepository(filePath);
    }
}
