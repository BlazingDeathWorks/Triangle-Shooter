using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyPoolManager : MonoBehaviour, IObjectPooler<EnemyCollision>
{
    public EnemyCollision Prefab => _enemy;
    public Queue<EnemyCollision> Pool { get; } = new Queue<EnemyCollision>();
    [SerializeField] private EnemyCollision _enemy;
    Vector2 _particlePos;

    public void OnPooled(EnemyCollision instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.position = _particlePos;
    }

    public void PoolMyEnemy(Vector2 particlePos)
    {
        _particlePos = particlePos;
        ObjectPool.Pool(this);
    }
}
