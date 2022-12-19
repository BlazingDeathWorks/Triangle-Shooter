using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModel : MonoBehaviour
{
    public Equipment EquipmentPrefab => _equipmentPrefab;
    public Sprite ModelDisplay => _modelDisplay;
    public Vector2 Offset => _offset;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Equipment _equipmentPrefab;
    [SerializeField] private Sprite _modelDisplay;

    public void InstantiateEquipmentPart()
    {
        Instantiate(_equipmentPrefab, Vector2.zero, Quaternion.identity);
    }
}
