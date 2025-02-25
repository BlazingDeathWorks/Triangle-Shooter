using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class EquipmentBullet<T> : BulletBase<T> where T : MonoBehaviour, IObjectPoolable<T>
{
    [SerializeField] protected ActionChannel BulletShotEventHandler;
    protected Transform Transform { get; private set; }
    private Vector2 _direction;
    private ScaleTween _scaleTween;
    private Collider2D _collider;

    private void OnEnable()
    {
        _scaleTween.ScaleObject();
    }

    protected override void Awake()
    {
        base.Awake();
        Transform = transform;
        _scaleTween = GetComponent<ScaleTween>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    protected override void FixedUpdateVirtual()
    {
        Rb.velocity = _direction.normalized * Speed;
    }

    protected override void OnReturnVirtual()
    {
        _collider.enabled = false;
        ReleaseBullet = false;
        gameObject.SetActive(false);
    }

    public void ReleaseEquipmentBulletPerpendicular(PlayerRotation playerRotation, int offsetFactor)
    {
        BulletShotEventHandler?.CallAction();
        _collider.enabled = true;
        ReleaseBullet = true;
        _direction = Vector2.Perpendicular(playerRotation.Direction) * offsetFactor;
        Transform.parent = null;
        Transform.localEulerAngles = new Vector3(0, 0, playerRotation.Angle - 90);
    }

    public void ReleaseEquipmentBullet(PlayerRotation playerRotation)
    {
        BulletShotEventHandler?.CallAction();
        _collider.enabled = true;
        ReleaseBullet = true;
        _direction = playerRotation.Direction;
        Transform.parent = null;
        Transform.localEulerAngles = new Vector3(0, 0, playerRotation.Angle - 90);
    }
}
