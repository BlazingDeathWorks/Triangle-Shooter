using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour, IObjectPooler<Bullet>, IUpgradable, IBonusApplicable
{
    public Bullet Prefab => _bullet;
    public Queue<Bullet> Pool { get; } = new Queue<Bullet>();

    [SerializeField] private ActionChannel _playerShotEventHandler;
    [SerializeField] private ActionChannel_Bool _buildActivatedEventHandler;
    [SerializeField] private Transform _bulletSpawnPoint = null;
    [SerializeField] private Bullet _bullet = null;
    [SerializeField] private ShakeTween _shakeTween = null;
    [SerializeField] private PlayerFireRateUpgradable _fireRateUpgradable;
    [SerializeField] private PlayerRotation _playerRotation;
    [SerializeField] private PlayerReloadUpgradable _reloadUpgradable;
    [SerializeField] private int _ammoPerRound = 3;
    [SerializeField] private int _ammoPerRoundBonus = 3;
    private int _currentAmmoCount = 3;


    private void Awake()
    {
        _currentAmmoCount = _ammoPerRound;
        _buildActivatedEventHandler?.AddAction(OnBuildActivated);
    }

    private void Update()
    {
        //Shooting
        _fireRateUpgradable.Tick();

        if (_currentAmmoCount != _ammoPerRound)
        {
            _reloadUpgradable.Tick();
            if (_reloadUpgradable.TimeSinceReloadStart >= _reloadUpgradable.ReloadSpeed)
            {
                _reloadUpgradable.ResetReloadTime();
                _currentAmmoCount = _ammoPerRound;
                _fireRateUpgradable.RefreshFireRate();
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_fireRateUpgradable.TimeSinceLastShot >= _fireRateUpgradable.TimeBetweenBullets && _currentAmmoCount > 0)
            {
                _playerShotEventHandler?.CallAction();
                _fireRateUpgradable.ResetLastShotTime();
                _reloadUpgradable.ResetReloadTime();
                _currentAmmoCount--;
                ObjectPool.Pool(this);
                _shakeTween?.Shake();
            }
        }
    }

    private void OnDestroy()
    {
        _buildActivatedEventHandler?.RemoveAction(OnBuildActivated);
    }

    private void OnBuildActivated(bool isActive)
    {
        if (isActive)
        {
            enabled = false;
            _fireRateUpgradable.RefreshFireRate();
            return;
        }
        enabled = true;
    }

    public void OnPooled(Bullet instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.position = _bulletSpawnPoint.position;
        instance.Direction = _playerRotation.Direction;
    }

    public void OnUpgrade()
    {
        _ammoPerRound *= 2;
        _currentAmmoCount = _ammoPerRound;
    }

    public void AddBonus()
    {
        _ammoPerRound += _ammoPerRoundBonus;
    }
}
