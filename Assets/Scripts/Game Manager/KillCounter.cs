using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class KillCounter : ScoreFactor
{
    public static KillCounter Instance { get; private set; }
    public int KillCount { get; private set; }
    protected override int ScoreMultiply => 5;
    [SerializeField] private ActionChannel _enemyDiedEventHandler;

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

    public override int CalculateScore()
    {
        return KillCount * ScoreMultiply;
    }
}
