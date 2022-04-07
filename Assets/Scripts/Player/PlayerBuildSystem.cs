using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerBuildSystem : MonoBehaviour, IObjectPooler<BuildingBlockBase>, IObjectPooler<BuildingBlock>
{
    public BuildingBlockBase Prefab => _buildingBlockBase;
    public Queue<BuildingBlockBase> Pool { get; } = new Queue<BuildingBlockBase>();

    BuildingBlock IObjectPooler<BuildingBlock>.Prefab => _buildingBlock;
    Queue<BuildingBlock> IObjectPooler<BuildingBlock>.Pool { get; } = new Queue<BuildingBlock>();

    [SerializeField] private InputButton _buildInput;
    [SerializeField] private Transform _buildBlockBaseContainer;
    [SerializeField] private BuildingBlockBase _buildingBlockBase;
    [SerializeField] private BuildingBlock _buildingBlock;
    private Transform _previousBlockPos;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_buildInput.Clicked)
        {
            ObjectPool.Pool<BuildingBlockBase>(this);
        }
    }

    public void OnPooled(BuildingBlockBase instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.parent = _buildBlockBaseContainer;
        instance.transform.position = _transform.position;
        if (_previousBlockPos == null)
        {
            _previousBlockPos = instance.transform;
            return;
        }
        ObjectPool.Pool<BuildingBlock>(this);
        _previousBlockPos = instance.transform;
    }

    public void OnPooled(BuildingBlock instance)
    {
        instance.gameObject.SetActive(true);
        float distance = Vector2.Distance(_previousBlockPos.position, _transform.position);
        instance.transform.localScale = new Vector2(distance, instance.transform.localScale.y);
        instance.transform.position = (_previousBlockPos.position + _transform.position) / 2;
        instance.transform.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(_transform.position.y - _previousBlockPos.position.y, _transform.position.x - _previousBlockPos.position.x));
    }
}
