using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BuildingBlock : MonoBehaviour, IObjectPoolable<BuildingBlock>
{
    public IObjectPooler<BuildingBlock> ParentObjectPooler { get; set; }
    [SerializeField] private ActionChannel _blockMaxHealthUpgradedEventHandler;
    private BuildingBlock _instance;
    private int _health = 1;

    private void Awake()
    {
        _instance = GetComponent<BuildingBlock>();
        _blockMaxHealthUpgradedEventHandler.AddAction(UpdateHealth);
    }

    private void OnEnable() 
    {
        UpdateHealth();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (--_health <= 0) 
        {
            ObjectPool.Return(this);
        }
    }

    private void UpdateHealth()
    {
        _health = PlayerBuildHealthUpgradable.MaxHealth;
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
