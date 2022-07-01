using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }
    public ActionChannel GameStartedEventHandler;
    [SerializeField] private EquipmentModelManager[] _modelManagers;

    private void Awake()
    {
        Instance = this;
    }

    public void SubscribeModels()
    {
        foreach (EquipmentModelManager manager in _modelManagers)
        {
            GameStartedEventHandler?.AddAction(manager.CurrentModel.InstantiateEquipmentPart);
        }
    }
}
