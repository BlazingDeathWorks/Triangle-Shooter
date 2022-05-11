using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Upgrade to fully heal player health
public class PlayerHealthSystem : MonoBehaviour, IUpgradable
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerMaxHealthUpgradable _playerMaxHealthUpgradable;
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] private ActionChannel _playerCollidedEventHandler;
    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private Slider _slider;
    [SerializeField] private LensTween _lensTween;

    private float _currentHealth = 3;

    private void Awake()
    {
        if (_playerMaxHealthUpgradable == null || _player == null || _lensTween == null) return;

        OnUpgrade();

        _playerCollidedEventHandler?.AddAction(() => _lensTween.DistortLens());
        _playerDiedEventHandler?.AddAction(() => Destroy(_player.gameObject));
        _playerDiedEventHandler?.AddAction(() => Instantiate(_particleSystem, transform.position, Quaternion.identity));
    }

    public void UpdateHealth()
    {
        if (_slider == null) return;
        _slider.value = _currentHealth / _playerMaxHealthUpgradable.MaxHealth;
    }

    public void CheckHealth()
    {
        if (_lensTween == null) return;

        _currentHealth--;
        UpdateHealth();
        _playerCollidedEventHandler?.CallAction();

        if (_currentHealth <= 0)
        {
            _playerDiedEventHandler?.CallAction();
        }
    }

    public void OnUpgrade()
    {
        _currentHealth = _playerMaxHealthUpgradable.MaxHealth;
        UpdateHealth();
    }
}
