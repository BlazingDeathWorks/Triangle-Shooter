using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BuildingBlock : MonoBehaviour
{
    [SerializeField] private ActionChannel _blockMaxHealthUpgradedEventHandler;
    private NormalPoolableObject _normalPoolableObject;
    private int _health = 1;

    private void Awake()
    {
        _normalPoolableObject = GetComponent<NormalPoolableObject>();
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
            ObjectPool.Return(_normalPoolableObject);
        }
    }

    private void UpdateHealth()
    {
        _health = PlayerBuildHealthUpgradable.MaxHealth;
    }
}
