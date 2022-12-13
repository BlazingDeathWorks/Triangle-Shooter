using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase<T> : MonoBehaviour, IObjectPoolable<T> where T : MonoBehaviour, IObjectPoolable<T>
{
    public IObjectPooler<T> ParentObjectPooler { get; set; }

    [SerializeField] protected bool ReleaseBullet = false;
    [SerializeField] protected float Speed = 5;
    protected Rigidbody2D Rb;
    [SerializeField] private float _lifetime = 3;
    private float _time = 0;
    private T _instance;

    private void Update()
    {
        if (!ReleaseBullet) return;

        _time += Time.deltaTime;
        if (_time < _lifetime) return;
        _time = 0;
        ObjectPool.Return(this);
    }

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        _instance = GetComponent<T>();
    }

    protected virtual void OnReturnAbstract()
    {
        gameObject.SetActive(false);
    }

    public void OnReturn()
    {
        OnReturnAbstract();
    }

    public T ReturnComponent()
    {
        return _instance;
    }
}
