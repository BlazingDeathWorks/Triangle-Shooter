using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class KillCounter : MonoBehaviour
{
    public static KillCounter Instance { get; private set; }
    [SerializeField] private ActionChannel _enemyDiedEventHandler;
    public int KillCount { get; private set; }

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        _enemyDiedEventHandler?.AddAction(CountKill);
    }

    private void OnDestroy()
    {
        _enemyDiedEventHandler?.RemoveAction(CountKill);
    }

    private void CountKill()
    {
        KillCount++;
    }
}
