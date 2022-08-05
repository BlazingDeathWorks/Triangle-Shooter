using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyVariantsManager : MonoBehaviour
{
    [SerializeField] private ActionChannel_Int _leveledUpEventHandler;
    [SerializeField] private EnemyVariantData[] _variantDatas;
    [SerializeField] private int[] _variantLevelDatas;
    private List<EnemyVariantData> _pickableVariants = new List<EnemyVariantData>();
    private int _index = 0;

    private void Awake()
    {
        _leveledUpEventHandler?.AddAction(UpdateVariantDatas);
        UpdateVariantDatas(1);
    }

    private void OnDestroy()
    {
        _leveledUpEventHandler?.RemoveAction(UpdateVariantDatas);
    }

    private void UpdateVariantDatas(int level)
    {
        if (_index >= _variantDatas.Length || level - 1 >= _variantLevelDatas.Length) return;
        for (int i = 0; i < _variantLevelDatas[level - 1]; i++)
        {
            _pickableVariants.Add(_variantDatas[_index]);
            _index++;
        }
    }

    public EnemyVariantData GetRandomVariant()
    {
        if (_pickableVariants.Count <= 0) return null;
        return _variantDatas[Random.Range(0, _pickableVariants.Count)];
    }
}