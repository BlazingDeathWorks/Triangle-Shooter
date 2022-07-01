using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SpeedGenerator : EquipmentGenerator<PlayerMovementController>
{
    [SerializeField] private float _speedBonus = 1.5f;
    
    protected override void Start()
    {
        base.Start();
        Component.AddToSpeed(_speedBonus);
    }
}
