using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyCollision : MonoBehaviour, IObjectPooler<EnemyDeathParticle>, IObjectPoolable<EnemyCollision>
{
    public IObjectPooler<EnemyCollision> ParentObjectPooler { get; set; }

    public EnemyDeathParticle Prefab => _deathParticle;
    public Queue<EnemyDeathParticle> Pool { get; } = new Queue<EnemyDeathParticle>();

    [SerializeField] private EnemyDeathParticle _deathParticle = null;
    [SerializeField] private float _collisionDistance = 0.1f;
    [SerializeField] private ActionChannel _enemieDeath;
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _flashTime = 0.1f;
    private int _health;
    private PlayerHealthSystem _playerHealthSystem;
    private SpriteRenderer _sr;
    private Transform _transform;
    private Transform _player;
    const string PLAYER = "Player";
    const string BULLET = "Bullet";
    private EnemyCollision _instance;
    private Color _originalSpriteColor;
    private Gradient _particleGradient;
    
    private void Awake()
    {
        _health = _maxHealth;
        _instance = GetComponent<EnemyCollision>();
        _sr = GetComponent<SpriteRenderer>();
        _originalSpriteColor = _sr.color;
        _transform = transform;
        _particleGradient = new Gradient();
        _particleGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(_sr.color, 0), new GradientColorKey(Color.white, 1) }, new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(0, 1) });
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
            StartCoroutine("Flash");
            if (--_health > 0) return;
            ObjectPool.Pool(this);
            ObjectPool.Return(this);
            _enemieDeath.CallAction();
        }
    }

    private IEnumerator Flash()
    {
        _sr.color = Color.white;
        yield return new WaitForSecondsRealtime(_flashTime);
        _sr.color = _originalSpriteColor;
    }

    public void OnReturn()
    {
        StopCoroutine("Flash");
        _health = _maxHealth;
        _sr.color = _originalSpriteColor;
        gameObject.SetActive(false);
    }

    public void OnPooled(EnemyDeathParticle instance)
    {
        //Change particle start color
        ParticleSystem.MainModule mainModule = instance.ReturnMainModule();
        mainModule.startColor = _sr.color;

        //Change particle color over lifetime
        ParticleSystem.ColorOverLifetimeModule colorOverLifeModule = instance.ReturnColorOverLifeModule();
        colorOverLifeModule.color = _particleGradient;

        instance.gameObject.SetActive(true);
        instance.transform.position = _transform.position;
    }

    public EnemyCollision ReturnComponent()
    {
        return _instance;
    }
}
