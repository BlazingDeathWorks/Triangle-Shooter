using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BuildGridBoxManager : MonoBehaviour, IObjectPooler<NormalPoolableObject>
{
    //Object Pooler Params
    public NormalPoolableObject Prefab => _buildBlockGhost;
    public Queue<NormalPoolableObject> Pool { get; } = new Queue<NormalPoolableObject>();
    [SerializeField] private NormalPoolableObject _buildBlockGhost;

    //Cache
    private Transform _transform;

    //Build Grid Box References
    private BuildGridBox _gridBoxHost;
    private Vector2 _gridBoxPos;

    private void Awake()
    {
        _transform = transform;
    }

    //Copy from BuildGridBox.cs
    public void OnPooled(NormalPoolableObject instance)
    {
        instance.gameObject.SetActive(true);
        _gridBoxHost.CurrentBuildBlockGhost = instance;
        instance.transform.parent = _transform;
        instance.transform.position = _gridBoxPos;
    }

    //Pool from this and set grid box reference to parameter
    public void PoolBlockGhost(BuildGridBox gridBoxHost, Vector2 gridBoxPos)
    {
        _gridBoxHost = gridBoxHost;
        _gridBoxPos = gridBoxPos;
        ObjectPool.Pool(this);
    }
}
