using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class Equipment : MonoBehaviour
{
    protected abstract string SceneReferenceKey { get; }

    private void Awake()
    {
        transform.parent = SceneReferenceManager.GetReference(SceneReferenceKey).transform;
    }
}
