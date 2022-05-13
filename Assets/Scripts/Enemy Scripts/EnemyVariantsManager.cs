using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyVariantsManager : MonoBehaviour
{
    [SerializeField] private EnemyVariantData[] _variantDatas;

    public EnemyVariantData GetRandomVariant()
    {
        if (_variantDatas.Length <= 0) return null;
        return _variantDatas[Random.Range(0, _variantDatas.Length)];
    }
}