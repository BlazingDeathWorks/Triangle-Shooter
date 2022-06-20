using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private FuncChannel_Bool _playerGhostRunnerBoolEventHandler;
    [SerializeField] private float _speed = 1;
    private Transform _transform;
    
    //Grid Movement State Machine
    private GridMovementDirectionStateMachine _stateMachine;
    //private GridMovementDirection[] _directions = new GridMovementDirection[] { new GridMovementUp(), new GridMovementDown(), new GridMovementLeft(), new GridMovementRight() };

    private void Awake()
    {
        _stateMachine = new GridMovementDirectionStateMachine();
        _transform = transform;
        _playerGhostRunnerBoolEventHandler.AddAction(() => Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0);
    }

    private void Update()
    {
        SetDirection();
    }

    private void FixedUpdate()
    {
        
    }

    private void SetDirection()
    {
        /*if (Input.GetKey(KeyCode.W)) _stateMachine.QueueNextState();
        if (Input.GetKey(KeyCode.S)) _stateMachine.QueueNextState();
        if (Input.GetKey(KeyCode.A)) _stateMachine.QueueNextState();
        if (Input.GetKey(KeyCode.D)) _stateMachine.QueueNextState();
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0 && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 0) _stateMachine.QueueNextState();*/
    }
}
