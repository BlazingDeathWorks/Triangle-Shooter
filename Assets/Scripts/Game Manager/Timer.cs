using UnityEngine;
using UnityEngine.UI;
using System;

internal class Timer : ScoreFactor
{
    public static Timer Instance { get; private set; }

    protected override int ScoreMultiply => 5;

    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] Text _text;
    private bool _canInrement = true;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        _playerDiedEventHandler?.AddAction(StopTimer);
    }

    private void Update()
    {
        if (!_canInrement) return;
        _text.text = (int)Time.timeSinceLevelLoad / 60 + ":" + ((int)Time.timeSinceLevelLoad % 60) / 10 + ((int)Time.timeSinceLevelLoad % 60) % 10;
    }

    private void OnDestroy()
    {
        _playerDiedEventHandler?.RemoveAction(StopTimer);
    }

    private void StopTimer()
    {
        _canInrement = false;
    }

    public override int CalculateScore()
    {
        //times will always be of a length of 2
        string[] times = _text.text.Split(':');
        int seconds = 0;
        try
        {
            seconds = ((int.Parse(times[0]) * 60) + int.Parse(times[1])) * ScoreMultiply;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        return seconds;
    }
}
