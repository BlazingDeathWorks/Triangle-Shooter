using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentBullet<T> : BulletBase<T> where T : MonoBehaviour, IObjectPoolable<T>
{
    [SerializeField] protected ActionChannel BulletShotEventHandler;
    protected Transform Transform;
    protected Vector2 Direction;

    protected override void FixedUpdateVirtual()
    {
        Rb.velocity = Direction.normalized * Speed;
    }

    public abstract void ReleaseRocket(PlayerRotation playerRotation);
}
