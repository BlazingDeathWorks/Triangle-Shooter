using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed =5f;
    private Rigidbody2D _rb;
    private Transform _player;
    const string PLAYER = "Player";
    void Awake() 
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(PLAYER)?.GetComponent<Transform>();
    }

    void Update() 
    {
        if (_player == null) return;

        var dir = _player.position - transform.position;
       
        //_rb.velocity = dir*_speed*Time.deltaTime;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _rb.AddForce(dir*_speed*Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
