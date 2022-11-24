using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerReloadUpgradable : MonoBehaviour, IUpgradable, IUpgradableVariants
{
    public object BonusFactor { get; set; } = 0f;
    public float ReloadSpeed => _reloadSpeed;
    public float TimeSinceReloadStart => _timeSinceReloadStart;
    [SerializeField] private float _reloadSpeedMinimum = 0.1f;
    [SerializeField] private float _reloadSpeed = 0.85f;
    private float _originalReloadSpeed;
    private float _timeSinceReloadStart;
    private float _percentFactor;

    private void Awake()
    {
        _originalReloadSpeed = _reloadSpeed;
    }

    public void Init(PowerData data)
    {
        _percentFactor = Random.Range(1, 4) / 10.0f + (float)BonusFactor;
        data.Description = $"Increases reload speed by {_percentFactor * 100}% of the current reload speed";
    }

    public void OnUpgrade()
    {
        _reloadSpeed -= _percentFactor * _reloadSpeed;
        _reloadSpeed = Mathf.Clamp(_reloadSpeed, _reloadSpeedMinimum, _originalReloadSpeed);
    }

    public void Tick()
    {
        _timeSinceReloadStart += Time.deltaTime;
    }

    public void ResetReloadTime()
    {
        _timeSinceReloadStart = 0;
    }
}
