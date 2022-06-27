using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Upgrade to increase max health of player
public class PlayerMaxHealthUpgradable : MonoBehaviour, IUpgradable, IUpgradableVariants
{
    public float MaxHealth { get; private set; } = 3;
    [SerializeField] private PlayerHealthSystem _playerHealthSystem;
    private float _percentFactor = 0;

    public void OnUpgrade()
    {
        MaxHealth += Mathf.Round(_percentFactor * MaxHealth);
        _playerHealthSystem?.UpdateHealth();
    }

    public void Init(PowerData data)
    {
        _percentFactor = Random.Range(1, 11) / 10.0f;
        data.Description = $"{_percentFactor}";
    }
}
