using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class GhostRunner : MonoBehaviour, IObjectPooler<NormalPoolableObject>
{
    public NormalPoolableObject Prefab => _prefab;
    public Queue<NormalPoolableObject> Pool { get; } = new Queue<NormalPoolableObject>();

    [SerializeField] private NormalPoolableObject _prefab;
    [SerializeField] private FuncChannel_Bool _ghostRunnerBoolEventHandler;
    [SerializeField] private Transform _ghostContainer;
    [SerializeField] private float _timeBetweenGhost = 1;
    private float _timeSinceLastGhost = 0;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _timeSinceLastGhost = _timeBetweenGhost;
    }

    private void Update()
    {
        if (_ghostRunnerBoolEventHandler == null || _ghostRunnerBoolEventHandler.GetSize() != 1 || !_ghostRunnerBoolEventHandler.CallAction()) return;
        if (_timeSinceLastGhost >= _timeBetweenGhost)
        {
            _timeSinceLastGhost = 0;
            ObjectPool.Pool(this);
            return;
        }
        _timeSinceLastGhost += Time.deltaTime;
    }

    public void OnPooled(NormalPoolableObject instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.parent = _ghostContainer;
        instance.transform.localEulerAngles = _transform.localEulerAngles;
        instance.transform.position = _transform.position;
    }
}
