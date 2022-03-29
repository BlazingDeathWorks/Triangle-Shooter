using System.Collections.Generic;
using UnityEngine;

internal class EnemyCollision : MonoBehaviour, IObjectPooler<EnemyDeathParticle>, IObjectPoolable<EnemyCollision>
{
    public IObjectPooler<EnemyCollision> ParentObjectPooler { get; set; }

    public EnemyDeathParticle Prefab => _deathParticle;
    public Queue<EnemyDeathParticle> Pool { get; } = new Queue<EnemyDeathParticle>();

    [SerializeField] private EnemyDeathParticle _deathParticle = null;
    [SerializeField] private float _collisionDistance = 0.1f;
    private Transform _transform;
    private Transform _player;
    const string PLAYER = "Player";
    const string BULLET = "Bullet";
    private ParticleSystem _playerDeathParticle;
    private EnemyCollision _instance;

    private void Awake()
    {
        _instance = GetComponent<EnemyCollision>();
        _transform = transform;
        _player = GameObject.FindGameObjectWithTag(PLAYER)?.GetComponent<Transform>();
        _playerDeathParticle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (_player == null) return;
        if (Vector2.Distance(_player.position, _transform.position) <= _collisionDistance)
        {
            _playerDeathParticle.Play();
            Destroy(_player.gameObject);
            Timer.StopTimer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(BULLET))
        {
            ObjectPool.Pool(this);
            ObjectPool.Return(this);
        }
    }

    public void OnReturn()
    {
        gameObject.SetActive(false);
    }

    public void OnPooled(EnemyDeathParticle instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.position = _transform.position;
    }

    public EnemyCollision ReturnComponent()
    {
        return _instance;
    }
}
