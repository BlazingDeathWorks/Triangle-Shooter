using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    public bool IsMoving => Mathf.Abs(_x) > 0 || Mathf.Abs(_y) > 0;
    [SerializeField] private InputButton[] _inputs = null;
    [SerializeField] private float _speed = 1;
    private int _x, _y;
    private Rigidbody2D _rb = null;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetXY();
    }

    private void FixedUpdate()
    {
        _rb.velocity = (new Vector2(_x, _y)).normalized * _speed;
        Debug.Log(Mathf.Abs(_x * _y));
    }

    private void SetXY()
    {
        //NONE
        if (!_inputs[0].Clicked && !_inputs[1].Clicked && !_inputs[2].Clicked && !_inputs[3].Clicked)
        {
            _x = 0;
            _y = 0;
            return;
        }

        //UP
        if (_inputs[0].Clicked)
        {
            _y = 1;
        }

        //DOWN
        if (_inputs[1].Clicked)
        {
            _y = -1;
        }

        //LEFT
        if (_inputs[2].Clicked)
        {
            _x = -1;
        }

        //RIGHT
        if (_inputs[3].Clicked)
        {
            _x = 1;
        }
    }
}
