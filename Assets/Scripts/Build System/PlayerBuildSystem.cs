using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerBuildSystem : MonoBehaviour, IObjectPooler<BuildingBlock>
{
    BuildingBlock IObjectPooler<BuildingBlock>.Prefab => _buildingBlock;
    Queue<BuildingBlock> IObjectPooler<BuildingBlock>.Pool { get; } = new Queue<BuildingBlock>();

    [SerializeField] private InputButton _buildInput;
    [SerializeField] private Transform _buildBlockBaseContainer;
    [SerializeField] private BuildingBlock _buildingBlock;
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
        instance.transform.parent = _buildBlockBaseContainer;
        //instance.transform.localScale = new Vector2(distance, instance.transform.localScale.y);
    }
}
