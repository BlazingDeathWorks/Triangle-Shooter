using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class RocketLauncher : MonoBehaviour, IObjectPooler<RocketHead>
{
    public RocketHead Prefab => _prefab;
    public Queue<RocketHead> Pool { get; } = new Queue<RocketHead>();
    [SerializeField] private RocketHead _prefab;
    private RocketHead _currentInstance;
    private Transform _parent;
    private Vector2 _pos;

    public void OnPooled(RocketHead instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.parent = _parent;
        instance.transform.localPosition = _pos;
        _currentInstance = instance;
    }

    public void DeployRocket(Transform parent, Vector2 pos)
    {
        _parent = parent;
        _pos = pos;
        ObjectPool.Pool(this);
    }

    public RocketHead GetInstance()
    {
        return _currentInstance;
    }
}
