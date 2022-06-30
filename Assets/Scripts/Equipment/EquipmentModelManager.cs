using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModelManager : MonoBehaviour
{
    [SerializeField] private Transform _equipmentPosition;
    private int _index = 0;
    private List<EquipmentModel> _equipmentModels = new List<EquipmentModel>();

    private void Awake()
    {
        //Disables everything except for the first children
        int i = 0;
        foreach (Transform child in transform)
        {
            if (i == _index) continue;
            child.gameObject.SetActive(false);
            _equipmentModels.Add(child.GetComponent<EquipmentModel>());
            i++;
        }
    }
}
