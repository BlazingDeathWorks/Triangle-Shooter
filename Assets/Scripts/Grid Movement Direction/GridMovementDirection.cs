using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridMovementDirection
{
    public Vector2 Destination { get; internal set; }
    public GridMovementDirection ReverseMovementDirection { get; set; }

    public abstract Vector2 ToVector();
    public abstract bool ReachedDestinationByTransform(Transform trans);
}

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

public class GridMovementUp : GridMovementDirection
{
    public override Vector2 ToVector()
    {
        return Vector2.up;
    }

    public override bool ReachedDestinationByTransform(Transform trans)
    {
        return trans.position.y >= Destination.y;
    }
}

public class GridMovementDown : GridMovementDirection
{
    public override Vector2 ToVector()
    {
        return Vector2.down;
    }

    public override bool ReachedDestinationByTransform(Transform trans)
    {
        return trans.position.y <= Destination.y;
    }
}

public class GridMovementLeft : GridMovementDirection
{
    public override Vector2 ToVector()
    {
        return Vector2.left;
    }

    public override bool ReachedDestinationByTransform(Transform trans)
    {
        return trans.position.x <= Destination.x;
    }
}

public class GridMovementRight : GridMovementDirection
{
    public override Vector2 ToVector()
    {
        return Vector2.right;
    }

    public override bool ReachedDestinationByTransform(Transform trans)
    {
        return trans.position.x >= Destination.x;
    }
}
