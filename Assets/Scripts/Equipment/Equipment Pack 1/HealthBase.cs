using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HealthBase : EquipmentBase
{
    protected override Type Type => typeof(PlayerHealthSystem);

    protected override float BonusFactor => 0.3f;
}
