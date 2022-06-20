using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private FuncChannel_Bool _playerGhostRunnerBoolEventHandler;
    [SerializeField] private float _speed = 1;
    private Transform _transform;
    private Rigidbody2D _rb;
    
    //Grid Movement State Machine
    private GridMovementDirectionStateMachine _stateMachine;
    private GridMovementDirection[] _directions = new GridMovementDirection[] { new GridMovementUp(), new GridMovementDown(), new GridMovementLeft(), new GridMovementRight() };

    private void Awake()
    {
        _stateMachine = new GridMovementDirectionStateMachine();
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _playerGhostRunnerBoolEventHandler.AddAction(() => Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0);
    }

    private void Update()
    {
        SetDirection();
        if (_stateMachine.CurrentGridMoveDir == null)
        {
            _stateMachine.SetNextDirectionState(_transform);
            return;
        }
        if (!_stateMachine.CurrentGridMoveDir.ReachedDestinationByTransform(_transform)) return;
        _transform.position = _stateMachine.CurrentGridMoveDir.Destination;
        _stateMachine.SetNextDirectionState(_transform);
    }

    private void FixedUpdate()
    {
        if (_rb == null) return;
        _rb.velocity = (_stateMachine.CurrentGridMoveDir != null ? _stateMachine.CurrentGridMoveDir.ToVector() : Vector2.zero) * _speed;
    }

    private void SetDirection()
    {
        if (Input.GetKey(KeyCode.W)) _stateMachine.QueueNextState(_directions[0]);
        if (Input.GetKey(KeyCode.S)) _stateMachine.QueueNextState(_directions[1]);
        if (Input.GetKey(KeyCode.A)) _stateMachine.QueueNextState(_directions[2]);
        if (Input.GetKey(KeyCode.D)) _stateMachine.QueueNextState(_directions[3]);
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0 && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 0) _stateMachine.QueueNextState(null);
    }
}
