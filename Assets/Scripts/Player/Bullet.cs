using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Bullet : MonoBehaviour, IObjectPoolable<Bullet>
{
    public Vector2 Direction { private get; set; }
    public IObjectPooler<Bullet> ParentObjectPooler { get; set; }

    [SerializeField] private float _speed = 5;
    [SerializeField] private float _lifetime = 3;
    private float _time = 0;
    private Rigidbody2D _rb = null;
    private TrailRenderer _trailRenderer = null;
    private Bullet _instance;

    private void Awake()
    {
        _instance = GetComponent<Bullet>();
        _rb = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time < _lifetime) return;
        _time = 0;
        ObjectPool.Return(this);
    }

    private void FixedUpdate()
    {
        _rb.velocity = Direction.normalized * _speed;
    }

    public void OnReturn()
    {
        gameObject.SetActive(false);
        _trailRenderer.Clear();
    }

    public Bullet ReturnComponent()
    {
        return _instance;
    }
}
