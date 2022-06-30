using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModel : MonoBehaviour
{
    private EquipmentModelManager _modelManager;

    private void Awake()
    {
        _modelManager = GetComponentInParent<EquipmentModelManager>();
    }
}
