using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private FuncChannel_Bool _playerGhostRunnerBoolEventHandler;
    [SerializeField] private float _speed = 1;
    private float _x, _y;
    private Transform _transform;
    private Rigidbody2D _rb = null;


    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _playerGhostRunnerBoolEventHandler.AddAction(() => Mathf.Abs(_x) > 0 || Mathf.Abs(_y) > 0);
    }

    private void Update()
    {
        SetXY();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition((Vector2)_transform.position + new Vector2(_x, _y).normalized * _speed * Time.deltaTime);
    }

    private void SetXY()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _y = Input.GetAxisRaw("Vertical");
    }
}
