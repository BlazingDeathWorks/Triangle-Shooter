using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase<Bullet>
{
    public Vector2 Direction { private get; set; }

    protected override void FixedUpdateVirtual()
    {
        Rb.velocity = Direction.normalized * Speed;
    }
}
