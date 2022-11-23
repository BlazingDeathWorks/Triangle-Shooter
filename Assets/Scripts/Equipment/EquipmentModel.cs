using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModel : MonoBehaviour
{
    public Equipment EquipmentPrefab => _equipmentPrefab;
    public Sprite ModelDisplay => _modelDisplay;
    [SerializeField] private Equipment _equipmentPrefab;
    [SerializeField] private Sprite _modelDisplay;

    public void InstantiateEquipmentPart()
    {
        Instantiate(_equipmentPrefab, Vector2.zero, Quaternion.identity);
    }
}
