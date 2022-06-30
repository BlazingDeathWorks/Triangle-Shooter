using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemySpawner : MonoBehaviour, IObjectPoolable<EnemySpawner>
{
    public IObjectPooler<EnemySpawner> ParentObjectPooler { get; set; }

    private ParticleSystem.MainModule _particleSettings;
    private EnemyVariantsManager _enemyVariantsManager;
    private EnemyVariantData _variantData;
    private ParticleSystem _particle;
    private EnemySpawner _instance;
    Transform _transform;
    private float _timeToSpawn;

    private void Awake()
    {
        _transform = transform;
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
            _variantData.EnemyPoolManager.PoolMyEnemy(_transform.position);
            ObjectPool.Return(this);
        }
    }

    public void OnReturn()
    {
        gameObject.SetActive(false);
    }

    public EnemySpawner ReturnComponent()
    {
        return _instance;
    }
}
