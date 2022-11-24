using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class AmmoBase : EquipmentBase<PlayerShootController>
{
    protected override object BonusFactor => 2;
}
