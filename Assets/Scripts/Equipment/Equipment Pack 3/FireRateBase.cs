using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class FireRateBase : EquipmentBase<PlayerFireRateUpgradable>
{
    protected override object BonusFactor => 0.2f;
}
