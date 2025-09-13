using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIManager : SingletonBehaviour<UIManager>
{
    [SerializeField] private Canvas _popupCanvas;
    [SerializeField] PopupUIListSO _popupUIList;

    private Transform _closeUIRoot;
    private Dictionary<Type, PopupUI> _openUIs = new();
    private Dictionary<Type, PopupUI> _closedUIs = new();
    private Dictionary<Type, PopupUI> _prefabTable = new();

    private void Awake()
    {
        base.Awake();

        var closeUIRootGo = new GameObject("@CloseUI");
        DontDestroyOnLoad(closeUIRootGo);
        _closeUIRoot = closeUIRootGo.transform;

        foreach (var prefab in _popupUIList.PopupUIs)
        {
            _prefabTable.Add(prefab.GetType(), prefab);
        }
    }

    public void Open<T>(PopupUIData data) where T : PopupUI
    {
        Type key = typeof(T);

        if (_prefabTable.ContainsKey(key) == false)
        {
            Debug.LogError($"{key.Name} is not registered.");
            return;
        }

        if (_openUIs.ContainsKey(key))
        {
            Debug.LogError($"{key.Name} is already opened.");
            return;
        }

        PopupUI ui = _closedUIs.ContainsKey(key) ? _closedUIs[key] : Instantiate(_prefabTable[key]);
        ui.transform.SetParent(_popupCanvas.transform);

        ui.Init();
        ui.Open(data);
        _openUIs.Add(key, ui);
        _closedUIs.Remove(key);
    }

    public void Close(PopupUI uiToClose)
    {
        Type key = uiToClose.GetType();

        if (_closedUIs.ContainsKey(key))
        {
            Debug.LogError($"{key.Name} is already closed.");
            return;
        }

        uiToClose.transform.SetParent(_closeUIRoot.transform);

        _openUIs.Remove(key);
        _closedUIs.Add(key, uiToClose);
    }

    public T Get<T>() where T : PopupUI
    {
        Type key = typeof(T);
        if (false == _openUIs.ContainsKey(key))
        {
            Debug.LogError($"{key.Name} is not opened yet.");

            return null;
        }

        return _openUIs[key] as T;
    }
}
