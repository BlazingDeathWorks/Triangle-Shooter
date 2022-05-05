using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private FuncChannel_Bool _playerGhostRunnerBoolEventHandler;
    [SerializeField] private float _speed = 1;
    private float _x, _y;
    private Rigidbody2D _rb = null;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerGhostRunnerBoolEventHandler.AddAction(() => Mathf.Abs(_x) > 0 || Mathf.Abs(_y) > 0);
    }

    private void Update()
    {
        SetXY();
    }

    private void FixedUpdate()
    {
        _rb.velocity = (new Vector2(_x, _y)).normalized * _speed;
    }

    private void SetXY()
    {
        _x = Input.GetAxis("Horizontal");
        _y = Input.GetAxis("Vertical");
    }
}
