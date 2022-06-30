using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModelManager : MonoBehaviour
{
    private void Awake()
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            if (i == 0) continue;
            child.gameObject.SetActive(false);
            i++;
        }
    }
}
