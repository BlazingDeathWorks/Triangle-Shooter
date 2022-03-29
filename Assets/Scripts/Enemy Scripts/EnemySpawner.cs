using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemySpawner : MonoBehaviour, IObjectPooler<EnemyCollision>, IObjectPoolable<EnemySpawner>
{
    public IObjectPooler<EnemySpawner> ParentObjectPooler { get; set; }
    public EnemyCollision Prefab => _enemy;
    public Queue<EnemyCollision> Pool { get; } = new Queue<EnemyCollision>();

    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private EnemyCollision _enemy;
    private EnemySpawner _instance;
    private float _timeToSpawn;

    private void Awake()
    {
        _instance = GetComponent<EnemySpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _timeToSpawn = 1f;
        _particle = GetComponent<ParticleSystem>();
        _particle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeToSpawn > 0)
        {
            _timeToSpawn -= Time.deltaTime;
        }
        else {
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
