using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    [SerializeField] [Tooltip("Define GameObjects that will be disabled if this tab is enabled")] 
    private GameObject[] _tabsToDisable;

    public void Enable()
    {
        foreach(GameObject tab in _tabsToDisable)
        {
            tab.SetActive(false);
        }
        gameObject.SetActive(true);
    }
}
