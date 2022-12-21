using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentOffsetController : MonoBehaviour
{
    [SerializeField] private Vector2 _offset;

    private void Start()
    {
        transform.localPosition = _offset;
    }
}
