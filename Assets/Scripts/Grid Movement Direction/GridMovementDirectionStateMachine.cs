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

    private void SetDestination(Vector2 position)
    {
        if (CurrentGridMoveDir == null) return;
        CurrentGridMoveDir.Destination = position + CurrentGridMoveDir.ToVector();
    }

    public void QueueNextState(GridMovementDirection nextDirection)
    {
        _nextDirection = nextDirection;
    }

    public void SetNextDirectionState(Vector2 position)
    {
        CurrentGridMoveDir = _nextDirection;
        SetDestination(position);
        _nextDirection = null;
    }
}
