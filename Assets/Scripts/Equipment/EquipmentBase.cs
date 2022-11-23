using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

internal abstract class EquipmentBase<T> : Equipment where T : MonoBehaviour, IUpgradableVariants
{
    protected sealed override string SceneReferenceKey => "Equipment Base";
    protected abstract float BonusFactor { get; }

    protected sealed override void Start()
    {
        base.Start();
        SceneReferenceManager.GetReference(PLAYER).GetComponentInChildren<T>().BonusFactor = BonusFactor;
    }
}
