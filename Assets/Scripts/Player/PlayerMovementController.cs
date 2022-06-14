using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private FuncChannel_Bool _playerGhostRunnerBoolEventHandler;
    [SerializeField] private float _speed = 1;
    private GridMovementDirection _currentGridDirection, _nextGridDirection;
    private Transform _transform;


    private void Awake()
    {
        _transform = transform;
        _playerGhostRunnerBoolEventHandler.AddAction(() => Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0);
    }

    private void Update()
    {
        SetDirection();
        MoveTowardsDirection();
    }

    private void MoveTowardsDirection()
    {
        //_transform.position = Vector2.MoveTowards(_transform.position, new Vector2((int)_transform.position.x, (int)_transform.position.y) + _direction, _speed * Time.deltaTime);
    }

    private void SetDirection()
    {
        /*if (Input.GetKey(KeyCode.W))
        {
            _direction = Vector2.up;
            return;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _direction = Vector2.down;
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _direction = Vector2.left;
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _direction = Vector2.right;
            return;
        }
        _direction = Vector2.zero;*/
    }
}
