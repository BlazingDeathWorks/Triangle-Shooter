using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class GridMovementDirection
{
    protected Vector2 Destination { get; set; }

    public abstract Vector2 ToVector();
    public abstract bool ReachedDestinationi(Transform trans);

    public void SetDestination(Transform trans)
    {
        Destination = (Vector2)trans.position + ToVector();
    }
}
