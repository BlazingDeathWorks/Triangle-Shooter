using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerBuildHealthUpgradable : MonoBehaviour, IUpgradable
{
    public static int MaxHealth { get; private set; } = 1;
    [SerializeField] private ActionChannel _blockMaxHealthUpgradedEventHandler;

    public void OnUpgrade()
    {
        MaxHealth++;
        _blockMaxHealthUpgradedEventHandler.CallAction();
    }
}
