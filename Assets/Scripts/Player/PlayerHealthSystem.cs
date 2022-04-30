using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Upgrade to fully heal player health
public class PlayerHealthSystem : MonoBehaviour, IUpgradable
{
    public static PlayerHealthSystem Instance { get; private set; }

    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerMaxHealthUpgradable _playerMaxHealthUpgradable;
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] private ActionChannel _playerCollidedEventHandler;
    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private Slider _slider;

    private float _currentHealth = 3;

    private void Awake()
    {
        #region Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion

        if (_playerMaxHealthUpgradable == null || _player == null) return;

        OnUpgrade();

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
        _currentHealth--;
        UpdateHealth();
        _playerCollidedEventHandler.CallAction();
        if (_currentHealth <= 0)
        {
            _playerDiedEventHandler.CallAction();
        }
    }

    public void OnUpgrade()
    {
        _currentHealth = _playerMaxHealthUpgradable.MaxHealth;
        UpdateHealth();
    }
}
