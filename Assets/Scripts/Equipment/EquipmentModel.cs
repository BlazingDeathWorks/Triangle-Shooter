using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModel : MonoBehaviour
{
    //Change type GameObject to Equipment Base class later...
    public GameObject EquipmentPrefab => _equipmentPrefab;
    public Sprite ModelDisplay => _modelDisplay;
    [SerializeField] private GameObject _equipmentPrefab;
    [SerializeField] private Sprite _modelDisplay;
}
