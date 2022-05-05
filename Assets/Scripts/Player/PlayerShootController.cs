using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerShootController : MonoBehaviour, IObjectPooler<Bullet>
{
    public Bullet Prefab => _bullet;
    public Queue<Bullet> Pool { get; } = new Queue<Bullet>();

    [SerializeField] Transform _bulletSpawnPoint = null;
    [SerializeField] Bullet _bullet = null;
    [SerializeField] Camera _camera = null;
    [SerializeField] ShakeTween _shakeTween = null;
    [SerializeField] float _timeBetweenBullets = 1;

    Transform _transform = null;
    Vector2 _mousePos, _direction;
    float _angle = 0;
    float _time;

    public void OnPooled(Bullet instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.position = _bulletSpawnPoint.position;
        instance.Direction = _direction;
    }

    private void Awake()
    {
        _transform = transform;
        _time = _timeBetweenBullets;
    }

    private void Update()
    {
        //Rotation
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _direction = _mousePos - (Vector2)_transform.position;
        _angle = Mathf.Rad2Deg * Mathf.Atan2(_direction.y, _direction.x);

        _transform.localEulerAngles = new Vector3(0, 0, _angle - 90);

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
}
