using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class EquipmentLauncher<T> : MonoBehaviour, IObjectPooler<T> where T : EquipmentBullet<T>
{
    public T Prefab => _prefab;
    public Queue<T> Pool { get; } = new Queue<T>();
    [SerializeField] private T _prefab;
    private T _currentInstance;
    private Transform _parent;
    private Vector2 _pos;

    public void OnPooled(T instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.parent = _parent;
        instance.transform.localPosition = _pos;
        instance.transform.localEulerAngles = Vector3.zero;
        _currentInstance = instance;
    }

    public void DeployRocket(Transform parent, Vector2 pos)
    {
        _parent = parent;
        _pos = pos;
        ObjectPool.Pool(this);
    }

    public EquipmentBullet<T> GetInstance()
    {
        return _currentInstance;
    }
}
