using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ReloadBase : EquipmentBase<PlayerReloadUpgradable>
{
    protected override object BonusFactor => 0.3f;
}
