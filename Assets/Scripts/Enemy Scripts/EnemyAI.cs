using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed =5f;
    private Transform _transform;
    private Transform _player;
    const string PLAYER = "Player";

    void Awake() 
    {
        _transform = transform;
        _player = GameObject.FindGameObjectWithTag(PLAYER)?.GetComponent<Transform>();
    }

    void Update() 
    {
        if (_player == null) return;
        _transform.position = Vector2.MoveTowards(_transform.position, _player.position, _speed * Time.deltaTime);
    }

}
