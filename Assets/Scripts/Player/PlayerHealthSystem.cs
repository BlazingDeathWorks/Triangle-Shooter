using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Upgrade to fully heal player health
public class PlayerHealthSystem : MonoBehaviour, IUpgradable, IUpgradableVariants
{
    public object BonusFactor { get; set; } = 0f;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerMaxHealthUpgradable _playerMaxHealthUpgradable;
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] private ActionChannel _playerCollidedEventHandler;
    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private Slider _slider;
    [SerializeField] private LensTween _lensTween;

    private float _currentHealth = 3;
    private float _percentFactor;

    private void Start()
    {
        if (_playerMaxHealthUpgradable == null || _player == null || _lensTween == null) return;

        _currentHealth = _playerMaxHealthUpgradable.MaxHealth;

        OnUpgrade();
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
        _lensTween.DistortLens();
        _playerCollidedEventHandler?.CallAction();

        if (_currentHealth <= 0)
        {
            Destroy(_player.gameObject);
            Instantiate(_particleSystem, transform.position, Quaternion.identity);
            _playerDiedEventHandler?.CallAction();
        }
    }

    public void OnUpgrade()
    {
        _currentHealth += Mathf.Ceil(_percentFactor * _playerMaxHealthUpgradable.MaxHealth);
        _currentHealth = Mathf.Clamp(_currentHealth, 1, _playerMaxHealthUpgradable.MaxHealth);
        UpdateHealth();
    }

    public void Init(PowerData data)
    {
        _percentFactor = Random.Range(1, 6) / 10.0f + (float)BonusFactor;
        data.Description = $"Increases health by {_percentFactor * 100}% of the current max health";
    }
}
