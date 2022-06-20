using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementDirectionStateMachine
{
    public GridMovementDirection CurrentGridMoveDir { get; private set; }
    private GridMovementDirection _nextDirection;

    public GridMovementDirectionStateMachine()
    {
        CurrentGridMoveDir = null;
        _nextDirection = null;
    }

    private void SetDestination(Transform trans)
    {
        if (CurrentGridMoveDir == null) return;
        CurrentGridMoveDir.Destination = (Vector2)trans.position + CurrentGridMoveDir.ToVector();
    }

    public void QueueNextState(GridMovementDirection nextDirection)
    {
        _nextDirection = nextDirection;
    }

    public void SetNextDirectionState(Transform trans)
    {
        CurrentGridMoveDir = _nextDirection;
        SetDestination(trans);
        _nextDirection = null;
    }
}
