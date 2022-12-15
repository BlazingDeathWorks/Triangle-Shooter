using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentBullet<T> : BulletBase<T> where T : MonoBehaviour, IObjectPoolable<T>
{
    [SerializeField] protected ActionChannel BulletShotEventHandler;
    private Transform _transform;
    private Vector2 _direction;
    private ScaleTween _scaleTween;
    private BoxCollider2D _boxCollider;

    private void OnEnable()
    {
        _scaleTween.ScaleObject();
    }

    protected override void Awake()
    {
        base.Awake();
        _transform = transform;
        _scaleTween = GetComponent<ScaleTween>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.enabled = false;
    }

    protected override void FixedUpdateVirtual()
    {
        Rb.velocity = _direction.normalized * Speed;
    }

    protected override void OnReturnVirtual()
    {
        _boxCollider.enabled = false;
        ReleaseBullet = false;
        gameObject.SetActive(false);
    }

    public void ReleaseEquipmentBullet(PlayerRotation playerRotation)
    {
        BulletShotEventHandler?.CallAction();
        _boxCollider.enabled = true;
        ReleaseBullet = true;
        _direction = playerRotation.Direction;
        _transform.parent = null;
        _transform.localEulerAngles = new Vector3(0, 0, playerRotation.Angle - 90);
    }
}
