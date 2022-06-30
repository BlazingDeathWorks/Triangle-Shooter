using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class Equipment : MonoBehaviour
{
    protected const string PLAYER = "Player";
    protected abstract string SceneReferenceKey { get; }

    protected virtual void Start()
    {
        transform.parent = SceneReferenceManager.GetReference(SceneReferenceKey).transform;
    }
}
