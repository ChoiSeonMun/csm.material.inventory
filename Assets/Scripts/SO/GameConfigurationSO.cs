using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigurationSO", menuName = "Scriptable Objects/GameConfigurationSO")]
public sealed class GameConfigurationSO : ScriptableObject
{
    public string ItemFilePath;
    public string UserInventoryFilePath;
}
