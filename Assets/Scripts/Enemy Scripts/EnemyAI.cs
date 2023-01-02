using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _magnitudeLimit = 8;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rushRate = 1;
    private Rigidbody2D _rb;
    private Transform _player;
    private Transform _transform;
    const string PLAYER = "Player";
    private float _timeSinceLastRush = 0;
    private Vector3 dir;

    void Awake() 
    {
        _transform = transform;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _player = SceneReferenceManager.GetReference(PLAYER).transform;
    }

    void Update() 
    {
        if (_player == null) return;

        _timeSinceLastRush += Time.deltaTime;
        dir = _player.position - _transform.position;

        if (_timeSinceLastRush >= _rushRate)
        {
            _timeSinceLastRush = 0;
            RushPlayer();
            return;
        }

        MoveTowardsPlayer();

    }

    private void MoveTowardsPlayer()
    {
        _rb.velocity = dir.normalized * _speed;
    }

    private void RushPlayer()
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (_rb.velocity.magnitude >= _magnitudeLimit)
        {
            _rb.AddForce(-dir * _speed);
        }
        _rb.AddForce(dir * _speed, ForceMode2D.Force);
        _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
