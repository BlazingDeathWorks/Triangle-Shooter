using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HealthBase : EquipmentBase<PlayerHealthSystem>
{
    protected override object BonusFactor => 0.3f;
}
