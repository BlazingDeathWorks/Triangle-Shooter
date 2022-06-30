using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

internal abstract class EquipmentBase : Equipment
{
    protected sealed override string SceneReferenceKey => "Equipment Base";
    protected abstract Type Type { get; }
    protected abstract float BonusFactor { get; }

    protected override void Start()
    {
        base.Start();
        ((IUpgradableVariants)SceneReferenceManager.GetReference(PLAYER).GetComponentInChildren(Type)).BonusFactor = BonusFactor;
    }
}
