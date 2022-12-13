using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
internal class RocketHead : MonoBehaviour, IObjectPoolable<RocketHead>
{
    [SerializeField] private ActionChannel _rocketShotEventHandler;
    //Same
    public IObjectPooler<RocketHead> ParentObjectPooler { get; set; }
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _lifetime = 3;
    private float _time = 0;
    private Rigidbody2D _rb;
    private bool _canReleaseRocket = false;
    //Same
    private Transform _transform;
    private ScaleTween _scaleTween;
    private BoxCollider2D _boxCollider;
    private Vector2 _direction;

    private void Awake()
    {
        //same
        _rb = GetComponent<Rigidbody2D>();
        //same
        _transform = transform;
        _boxCollider = GetComponent<BoxCollider2D>();
        _scaleTween = GetComponent<ScaleTween>();
        _boxCollider.enabled = false;
    }

    private void OnEnable()
    {
        _scaleTween.ScaleObject();
    }

    //same
    private void Update()
    {
        if (!_canReleaseRocket) return;

        _time += Time.deltaTime;
        if (_time < _lifetime) return;
        _time = 0;
        ObjectPool.Return(this);
    }
    //same

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
        _rocketShotEventHandler?.CallAction();
        _boxCollider.enabled = true;
        _canReleaseRocket = true;
        _direction = playerRotation.Direction;
        _transform.parent = null;
        _transform.localEulerAngles = new Vector3(0, 0, playerRotation.Angle - 90);
    }
}
