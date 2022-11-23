using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Upgrade to increase max health of player
public class PlayerMaxHealthUpgradable : MonoBehaviour, IUpgradable, IUpgradableVariants, IBonusApplicable
{
    public float BonusFactor { get; set; } = 0;
    public float MaxHealth { get; private set; } = 3;

    [SerializeField] private ActionChannel_Float _maxHealthUpgradedEventHandler;
    [SerializeField] private PlayerHealthSystem _playerHealthSystem;
    [SerializeField] private float _maxHealthBonus = 3;
    private float _percentFactor = 1;

    private void Start()
    {
        _maxHealthUpgradedEventHandler?.CallAction(MaxHealth);
    }

    public void OnUpgrade()
    {
        MaxHealth += Mathf.Ceil(_percentFactor * MaxHealth);
        _playerHealthSystem?.UpdateHealth();
        _maxHealthUpgradedEventHandler?.CallAction(MaxHealth);
    }

    public void Init(PowerData data)
    {
        _percentFactor = Random.Range(1, 6) / 10.0f + BonusFactor;
        data.Description = $"Increases max health by {_percentFactor * 100}% of the current max health";
    }

    public void AddBonus()
    {
        MaxHealth += _maxHealthBonus;
    }
}
