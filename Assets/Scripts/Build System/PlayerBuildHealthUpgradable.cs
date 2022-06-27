using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerBuildHealthUpgradable : MonoBehaviour, IUpgradable, IUpgradableVariants
{
    public static int MaxHealth { get; private set; } = 1;
    [SerializeField] private ActionChannel _blockMaxHealthUpgradedEventHandler;
    private float _percentFactor = 0;

    public void OnUpgrade()
    {
        MaxHealth += (int)Mathf.Round(_percentFactor * MaxHealth);
        _blockMaxHealthUpgradedEventHandler.CallAction();
    }

    public void Init(PowerData data)
    {
        _percentFactor = Random.Range(1, 11) / 10.0f;
        data.Description = $"{_percentFactor}";
    }
}
