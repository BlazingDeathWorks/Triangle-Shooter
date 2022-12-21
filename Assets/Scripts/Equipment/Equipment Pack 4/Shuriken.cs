using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Shuriken : EquipmentBullet<Shuriken>
{
    [SerializeField] private float _reverseTime;
    private float _time;
    private bool _first = false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        _time += Time.deltaTime;
        if (_time >= _reverseTime)
        {
            _time = 0;
            Speed = -Speed;
            if (_first) return;
            _first = true;
            _reverseTime *= 2.5f;
        }
    }
}
