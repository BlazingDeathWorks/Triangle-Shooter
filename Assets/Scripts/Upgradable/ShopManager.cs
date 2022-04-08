using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _powerTabs;
    [SerializeField] private GameObject[] _shopItems;

    private void Awake()
    {
        if (_powerTabs.Length != _shopItems.Length)
        {
            return;
        }
        foreach (GameObject item in _shopItems)
        {
            if (item.TryGetComponent<IUpgradable>(out IUpgradable upgradable))
            {
                upgradable.OnUpgrade();
            }
        }
    }
}
