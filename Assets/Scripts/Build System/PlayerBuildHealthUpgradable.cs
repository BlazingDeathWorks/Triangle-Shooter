using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerBuildHealthUpgradable : MonoBehaviour, IUpgradable
{
    public static int MaxHealth { get; private set; } = 8;
    [SerializeField] private ActionChannel _blockMaxHealthUpgradedEventHandler;

    public void OnUpgrade()
    {
        MaxHealth += 4;
        _blockMaxHealthUpgradedEventHandler.CallAction();
    }
}
