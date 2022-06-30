using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModel : MonoBehaviour
{
    //Change type GameObject to Equipment Base class later...
    [SerializeField] private GameObject _equipmentPrefab;
    [SerializeField] private Sprite _modelDisplay;
    private EquipmentModelManager _modelManager;

    private void Awake()
    {
        _modelManager = GetComponentInParent<EquipmentModelManager>();
    }
}
