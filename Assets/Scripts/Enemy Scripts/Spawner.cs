using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Spawner : MonoBehaviour, IObjectPooler<EnemySpawner>
{
    public EnemySpawner Prefab => _particle;
    public Queue<EnemySpawner> Pool { get; } = new Queue<EnemySpawner>();

    [SerializeField] EnemySpawner _particle = null;  

    //Spawn Difficulty
    private const float SPAWN_RATE_MIN = 0.4f, SPAWN_RATE_MAX = 0.8f;
    private const int FREQUENCY = 20;
    private float _spawnRate = 0.8f;
    private float _decrementBy = 0.1f;
    private int _spawnRateDecrementRate = 20;

    IEnumerator Start()
    {
        while (true)
        {
            if (Time.time * 10 >= _spawnRateDecrementRate)
            {
                _spawnRateDecrementRate += FREQUENCY;
                _spawnRate -= _decrementBy;
                _spawnRate = MathUtil.WrapFloat(_spawnRate, SPAWN_RATE_MIN, SPAWN_RATE_MAX);
            }
            yield return new WaitForSeconds(_spawnRate);
            ObjectPool.Pool(this);
        }
    }

    public void OnPooled(EnemySpawner instance)
    {
        instance.gameObject.SetActive(true);
        Vector3 topCorner = BoundsManager.GetCornerPosition(0);
        Vector3 bottomCorner = BoundsManager.GetCornerPosition(1);
        Vector2 pos = new Vector2(Random.Range(bottomCorner.x, topCorner.x), Random.Range(bottomCorner.y, topCorner.y));
        instance.transform.position = pos;
    }
}
