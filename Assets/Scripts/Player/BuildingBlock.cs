using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour, IObjectPoolable<BuildingBlock>
{
    public IObjectPooler<BuildingBlock> ParentObjectPooler { get; set; }
    private BuildingBlock _instance;

    private void Awake()
    {
        _instance = GetComponent<BuildingBlock>();
    }

    public void OnReturn()
    {
        gameObject.SetActive(false);
    }

    public BuildingBlock ReturnComponent()
    {
        return _instance;
    }
}
