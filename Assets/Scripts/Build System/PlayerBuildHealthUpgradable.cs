using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildHealthUpgradable : MonoBehaviour, IUpgradable, IBonusApplicable
{
    public static int MaxHealth { get; private set; } = 8;
    [SerializeField] private ActionChannel _blockMaxHealthUpgradedEventHandler;
    [SerializeField] private int _maxHealthBonus = 2;

    public void OnUpgrade()
    {
        MaxHealth += 4;
        _blockMaxHealthUpgradedEventHandler.CallAction();
    }

    public void AddBonus()
    {
        MaxHealth += _maxHealthBonus;
    }
}
