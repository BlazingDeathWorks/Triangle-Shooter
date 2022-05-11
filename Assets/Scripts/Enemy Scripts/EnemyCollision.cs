using System.Collections.Generic;
using UnityEngine;

internal class EnemyCollision : MonoBehaviour, IObjectPooler<EnemyDeathParticle>, IObjectPoolable<EnemyCollision>
{
    public IObjectPooler<EnemyCollision> ParentObjectPooler { get; set; }

    public EnemyDeathParticle Prefab => _deathParticle;
    public Queue<EnemyDeathParticle> Pool { get; } = new Queue<EnemyDeathParticle>();

    [SerializeField] private EnemyDeathParticle _deathParticle = null;
    [SerializeField] private float _collisionDistance = 0.1f;
    private PlayerHealthSystem _playerHealthSystem;
    private Transform _transform;
    private Transform _player;
    const string PLAYER = "Player";
    const string BULLET = "Bullet";
    private EnemyCollision _instance;

    private void Awake()
    {
        _instance = GetComponent<EnemyCollision>();
        _transform = transform;
    }

    private void Start()
    {
        GameObject player = SceneReferenceManager.GetReference(PLAYER);
        if (player == null) return;
        _player = player.GetComponent<Transform>();
        _playerHealthSystem = player.GetComponentInChildren<PlayerHealthSystem>();
    }

    //Destroying Player
    private void Update()
    {
        if (_player == null) return;
        if (Vector2.Distance(_player.position, _transform.position) <= _collisionDistance)
        {
            _playerHealthSystem?.CheckHealth();
            ObjectPool.Return(this);
        }
    }

    //Destroying Enemy by Bullet Collision
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
