using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class GridMovementDirectionStateMachine
{
    private GridMovementDirection _currentGridMoveDir, _nextDirection;

    public GridMovementDirectionStateMachine()
    {
        //_currentGridMoveDir = new GridMovementDirection();
    }

    public void QueueNextState(GridMovementDirection nextDirection)
    {
        _nextDirection = nextDirection;
    }
}
