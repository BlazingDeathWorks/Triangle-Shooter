using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public static event Action TimeFinishedEventHandler;
    [SerializeField] Text _text;
    private static bool _canInrement = true;
    private float _boundsIncreaseRate = 60, _originalBoundsIncreaseRate;
    private int count = 0;

    private void Awake()
    {
        _originalBoundsIncreaseRate = _boundsIncreaseRate;
    }

    private void Update()
    {
        if (!_canInrement) return;
        _text.text = (int)Time.time / 60 + ":" + ((int)Time.time % 60) / 10 + ((int)Time.time % 60) % 10;
        if (Time.time < _boundsIncreaseRate) return;
        TimeFinishedEventHandler?.Invoke();
        _boundsIncreaseRate = Mathf.Pow(2, ++count) * _originalBoundsIncreaseRate;
    }

    public static void StopTimer()
    {
        _canInrement = false;
    }
}
