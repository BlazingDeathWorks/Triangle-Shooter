using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Upgrade to increase max health of player
public class PlayerMaxHealthUpgradable : MonoBehaviour, IUpgradable, IUpgradableVariants
{
    public float BonusFactor { get; set; } = 0;
    public float MaxHealth { get; private set; } = 1000;
    
    [SerializeField] private PlayerHealthSystem _playerHealthSystem;
    private float _percentFactor = 0;

    public void OnUpgrade()
    {
        MaxHealth += Mathf.Round(_percentFactor * MaxHealth);
        _playerHealthSystem?.UpdateHealth();
    }

    public void Init(PowerData data)
    {
        _percentFactor = Random.Range(1, 11) / 10.0f + BonusFactor;
        data.Description = $"{_percentFactor}";
    }
}
