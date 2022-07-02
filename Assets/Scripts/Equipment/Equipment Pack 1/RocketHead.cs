using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
internal class RocketHead : MonoBehaviour, IObjectPoolable<RocketHead>
{
    public IObjectPooler<RocketHead> ParentObjectPooler { get; set; }
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _lifetime = 3;
    private Transform _transform;
    private ScaleTween _scaleTween;
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private Vector2 _direction;
    private float _time = 0;
    private bool _canReleaseRocket = false;

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _scaleTween = GetComponent<ScaleTween>();
        _boxCollider.enabled = false;
    }

    private void OnEnable()
    {
        _scaleTween.ScaleObject();
    }

    private void Update()
    {
        if (!_canReleaseRocket) return;

        _time += Time.deltaTime;
        if (_time < _lifetime) return;
        _time = 0;
        ObjectPool.Return(this);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction.normalized * _speed;
    }

    public void OnReturn()
    {
        _boxCollider.enabled = false;
        _canReleaseRocket = false;
        gameObject.SetActive(false);
    }

    public RocketHead ReturnComponent()
    {
        return this;
    }

    public void ReleaseRocket(PlayerRotation playerRotation)
    {
        _boxCollider.enabled = true;
        _canReleaseRocket = true;
        _direction = playerRotation.Direction;
        _transform.parent = null;
        _transform.localEulerAngles = new Vector3(0, 0, playerRotation.Angle - 90);
    }
}
