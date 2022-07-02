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
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public void SubscribeModels()
    {
        foreach (EquipmentModelManager manager in _modelManagers)
        {
            GameStartedEventHandler?.AddAction(manager.CurrentModel.InstantiateEquipmentPart);
        }
    }
}
