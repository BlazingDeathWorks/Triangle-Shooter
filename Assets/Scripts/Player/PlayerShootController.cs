using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerShootController : MonoBehaviour, IObjectPooler<Bullet>
{
    public Bullet Prefab => _bullet;
    public Queue<Bullet> Pool { get; } = new Queue<Bullet>();

    [SerializeField] private ActionChannel_Bool _buildActivatedEventHandler;
    [SerializeField] private Transform _bulletSpawnPoint = null;
    [SerializeField] private Bullet _bullet = null;
    [SerializeField] private ShakeTween _shakeTween = null;
    [SerializeField] private float _timeBetweenBullets = 1;
    private float _time;
    private PlayerRotation _playerRotation;

    private void Awake()
    {
        _playerRotation = GetComponent<PlayerRotation>();
        _buildActivatedEventHandler?.AddAction(OnBuildActivated);
        _time = _timeBetweenBullets;
    }

    private void Update()
    {
        //Shooting
        _time += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_time >= _timeBetweenBullets)
            {
                _time = 0;
                ObjectPool.Pool(this);
                _shakeTween?.Shake();
            }
        }
    }

    private void OnBuildActivated(bool isActive)
    {
        if (isActive)
        {
            enabled = false;
            _time = _timeBetweenBullets;
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
}
