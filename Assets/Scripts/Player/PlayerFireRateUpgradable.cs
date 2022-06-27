using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerFireRateUpgradable : MonoBehaviour, IUpgradable, IUpgradableVariants
{
    public float TimeBetweenBullets => _timeBetweenBullets;
    [SerializeField] private float _timeBetweenBulletsMinimum = 0.1f;
    [SerializeField] private float _timeBetweenBullets = 0.2f;
    private float _originalTimeBetweenBullets = 0.2f;
    private float _percentFactor = 0;

    private void Awake()
    {
        _originalTimeBetweenBullets = _timeBetweenBullets;
    }

    public void Init(PowerData data)
    {
        _percentFactor = Random.Range(1, 4) / 10.0f;
        data.Description = $"{_percentFactor}";
    }

    public void OnUpgrade()
    {
        _timeBetweenBullets -= _percentFactor * _timeBetweenBullets;
        _timeBetweenBullets = Mathf.Clamp(_timeBetweenBullets, _timeBetweenBulletsMinimum, _originalTimeBetweenBullets);
    }
}
