using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MaxHealthScoreCalculator : ScoreFactor
{
    public static MaxHealthScoreCalculator Instance { get; private set; }

    protected override int ScoreMultiply => 3;
    [SerializeField] private ActionChannel_Float _maxHealthUpgradedEventHandler;
    private const int STARTING_BONUS = 30000;
    private float _maxHealth = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _maxHealthUpgradedEventHandler?.AddAction(SetMaxHealth);
    }

    private void OnDestroy()
    {
        _maxHealthUpgradedEventHandler?.RemoveAction(SetMaxHealth);
    }

    private void SetMaxHealth(float value)
    {
        _maxHealth = value;
    }

    public override int CalculateScore()
    {
        return (int)(STARTING_BONUS / _maxHealth);
    }
}
