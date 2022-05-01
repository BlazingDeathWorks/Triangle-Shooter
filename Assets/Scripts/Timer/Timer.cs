using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public static event Action TimeFinishedEventHandler;
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] Text _text;
    private bool _canInrement = true;
    private float _boundsIncreaseRate = 60, _originalBoundsIncreaseRate;
    private int count = 0;

    private void Awake()
    {
        _playerDiedEventHandler?.AddAction(StopTimer);
        _originalBoundsIncreaseRate = _boundsIncreaseRate;
    }

    private void Update()
    {
        if (!_canInrement) return;
        _text.text = (int)Time.timeSinceLevelLoad / 60 + ":" + ((int)Time.timeSinceLevelLoad % 60) / 10 + ((int)Time.timeSinceLevelLoad % 60) % 10;
        if (Time.timeSinceLevelLoad < _boundsIncreaseRate) return;
        TimeFinishedEventHandler?.Invoke();
        _boundsIncreaseRate = Mathf.Pow(2, ++count) * _originalBoundsIncreaseRate;
    }

    private void StopTimer()
    {
        _canInrement = false;
    }
}
