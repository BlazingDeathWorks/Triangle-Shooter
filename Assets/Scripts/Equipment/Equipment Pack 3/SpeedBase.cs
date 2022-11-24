using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SpeedBase : EquipmentBase<PlayerMovementController>
{
    protected override object BonusFactor => 0.2f;
}
