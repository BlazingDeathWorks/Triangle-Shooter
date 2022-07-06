using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerFireRateUpgradable : MonoBehaviour, IUpgradable, IUpgradableVariants
{
    public float BonusFactor { get; set; } = 0;
    public float TimeBetweenBullets => _timeBetweenBullets;
    public float TimeSinceLastShot => _timeSinceLastShot;
    [SerializeField] private float _timeBetweenBulletsMinimum = 0.1f;
    [SerializeField] private float _timeBetweenBullets = 0.2f;
    private float _timeSinceLastShot;
    private float _originalTimeBetweenBullets = 0.2f;
    private float _percentFactor = 0;

    private void Awake()
    {
        _originalTimeBetweenBullets = _timeBetweenBullets;
        RefreshFireRate();
    }

    public void Init(PowerData data)
    {
        _percentFactor = Random.Range(1, 4) / 10.0f + BonusFactor;
        data.Description = $"Increases fire rate by {_percentFactor * 100}% of the current fire rate";
    }

    public void OnUpgrade()
    {
        _timeBetweenBullets -= _percentFactor * _timeBetweenBullets;
        _timeBetweenBullets = Mathf.Clamp(_timeBetweenBullets, _timeBetweenBulletsMinimum, _originalTimeBetweenBullets);
    }

    public void Tick()
    {
        _timeSinceLastShot += Time.deltaTime;
    }

    public void ResetLastShotTime()
    {
        _timeSinceLastShot = 0;
    }

    public void RefreshFireRate()
    {
        _timeSinceLastShot = _timeBetweenBullets;
    }
}
