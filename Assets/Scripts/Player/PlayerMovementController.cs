using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private ActionChannel_Bool _buildActivatedEventHandler;
    [SerializeField] private FuncChannel_Bool _playerGhostRunnerBoolEventHandler;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _destinationWaitTime = 0.4f;
    private float _timeSinceReachedLastDestination;
    private PlayerBuildSystem _buildSystem;
    private Transform _transform;
    private Rigidbody2D _rb;
    
    //Grid Movement State Machine
    private GridMovementDirectionStateMachine _stateMachine;
    private GridMovementDirection[] _directions;

    private void Awake()
    {
        //State Machine Config
        GridMovementUp up = new GridMovementUp();
        GridMovementDown down = new GridMovementDown();
        GridMovementLeft left = new GridMovementLeft();
        GridMovementRight right = new GridMovementRight();

        up.ReverseMovementDirection = down;
        down.ReverseMovementDirection = up;
        left.ReverseMovementDirection = right;
        right.ReverseMovementDirection = left;

        _directions = new GridMovementDirection[] { up, down, left, right };
        _stateMachine = new GridMovementDirectionStateMachine();

        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _buildSystem = GetComponentInChildren<PlayerBuildSystem>();
        _playerGhostRunnerBoolEventHandler.AddAction(() => Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0);
        _buildActivatedEventHandler.AddAction(OnBuildActivated);
    }

    private void Update()
    {
        SetDirection();
        if (_stateMachine.CurrentGridMoveDir == null)
        {
            _buildSystem.enabled = true;
            _stateMachine.SetNextDirectionState(_transform.position);
            return;
        }
        if (!_stateMachine.CurrentGridMoveDir.ReachedDestinationByTransform(_transform))
        {
            _buildSystem.enabled = false;
            _timeSinceReachedLastDestination += Time.deltaTime;
            if (_timeSinceReachedLastDestination >= _destinationWaitTime)
            {
                _timeSinceReachedLastDestination = 0;
                _stateMachine.QueueNextState(_stateMachine.CurrentGridMoveDir.ReverseMovementDirection);
                _stateMachine.SetNextDirectionState(_stateMachine.CurrentGridMoveDir.Destination);
            }
            return;
        }
        _timeSinceReachedLastDestination = 0;
        _buildSystem.enabled = true;
        _transform.position = _stateMachine.CurrentGridMoveDir.Destination;
        _stateMachine.SetNextDirectionState(_transform.position);
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

    private void OnBuildActivated(bool isActive)
    {
        if (isActive)
        {
            enabled = false;
            return;
        }
        enabled = true;
    }
}
