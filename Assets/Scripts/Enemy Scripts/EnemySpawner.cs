using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemySpawner : MonoBehaviour, IObjectPooler<EnemyCollision>, IObjectPoolable<EnemySpawner>
{
    public IObjectPooler<EnemySpawner> ParentObjectPooler { get; set; }
    public EnemyCollision Prefab => _enemy;
    public Queue<EnemyCollision> Pool { get; } = new Queue<EnemyCollision>();

    private ParticleSystem.MainModule _particleSettings;
    private EnemyVariantsManager _enemyVariantsManager;
    private EnemyVariantData _variantData;
    private ParticleSystem _particle;
    private EnemyCollision _enemy;
    private EnemySpawner _instance;
    private float _timeToSpawn;

    private void Awake()
    {
        _instance = GetComponent<EnemySpawner>();
        _particle = GetComponent<ParticleSystem>();
        _particleSettings = _particle.main;
        _enemyVariantsManager = SceneReferenceManager.GetReference("Enemy Variants Manager")?.GetComponent<EnemyVariantsManager>();
        _timeToSpawn = 1f;
    }

    private void OnEnable()
    {
        if (_particle == null) return;
        _variantData = _enemyVariantsManager.GetRandomVariant();
        _particleSettings.startColor = _variantData.Color;
        _enemy = _variantData.EnemyCollision;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeToSpawn > 0)
        {
            _timeToSpawn -= Time.deltaTime;
        }
        else
        {
            _timeToSpawn = 1;
            ObjectPool.Pool(this);
            ObjectPool.Return(this);
        }
    }

    public void OnReturn()
    {
        gameObject.SetActive(false);
    }

    public void OnPooled(EnemyCollision instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.position = transform.position;
    }

    public EnemySpawner ReturnComponent()
    {
        return _instance;
    }
}
