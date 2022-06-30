using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModel : MonoBehaviour
{
    public Sprite ModelDisplay => _modelDisplay;
    //Change type GameObject to Equipment Base class later...
    [SerializeField] private GameObject _equipmentPrefab;
    [SerializeField] private Sprite _modelDisplay;
}
