using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class EquipmentBranch : Equipment
{
    [SerializeField] private string _sceneReferenceKey;
    protected override string SceneReferenceKey => _sceneReferenceKey;
}
