using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentManager : MonoBehaviour
{
    private ActionChannel _gameStartedEventHandler;
    [SerializeField] private EquipmentModelManager[] _modelManagers;

    private void Awake()
    {
        _gameStartedEventHandler.ClearAll();
    }

    public void SubscribeModels()
    {
        foreach (EquipmentModelManager manager in _modelManagers)
        {
            _gameStartedEventHandler?.AddAction(manager.CurrentModel.InstantiateEquipmentPart);
        }
    }
}
