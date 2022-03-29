using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerBuildSystem : MonoBehaviour, IObjectPooler<BuildingBlock>
{
    public BuildingBlock Prefab => _buildBlock;
    public Queue<BuildingBlock> Pool { get; } = new Queue<BuildingBlock>();
    [SerializeField] private InputButton _buildInput;
    [SerializeField] private Transform _buildBlockContainer;
    [SerializeField] private BuildingBlock _buildBlock;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_buildInput.Clicked)
        {
            ObjectPool.Pool(this);
        }
    }

    public void OnPooled(BuildingBlock instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.parent = _buildBlockContainer;
        instance.transform.position = _transform.position;
    }
}
