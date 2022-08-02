using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class EquipmentGenerator<T> : Equipment where T : GeneratorBonusApplier
{
    protected sealed override string SceneReferenceKey => "Equipment Generator";

    protected override void Start()
    {
        base.Start();
        SceneReferenceManager.GetReference(PLAYER).GetComponentInChildren<T>().AddBonus();
    }
}
