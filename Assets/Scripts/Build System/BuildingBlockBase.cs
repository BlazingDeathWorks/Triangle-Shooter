using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BuildingBlockBase : MonoBehaviour, IObjectPoolable<BuildingBlockBase>
{
    public IObjectPooler<BuildingBlockBase> ParentObjectPooler { get; set; }
    private BuildingBlockBase _instance;

    private void Awake()
    {
        _instance = GetComponent<BuildingBlockBase>();
    }

    public void OnReturn()
    {
        gameObject.SetActive(false);
    }

    public BuildingBlockBase ReturnComponent()
    {
        return _instance;
    }
}
