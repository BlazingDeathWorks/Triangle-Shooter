using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerBuildHealthUpgradable : MonoBehaviour, IUpgradable
{
    public static int MaxHealth { get; private set; } = 2;
    [SerializeField] private ActionChannel _blockMaxHealthUpgradedEventHandler;

    public void OnUpgrade()
    {
        MaxHealth += 2;
        _blockMaxHealthUpgradedEventHandler.CallAction();
    }
}
