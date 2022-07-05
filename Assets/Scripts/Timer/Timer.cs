using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] Text _text;
    private bool _canInrement = true;

    private void Awake()
    {
        _playerDiedEventHandler?.AddAction(StopTimer);
    }

    private void Update()
    {
        if (!_canInrement) return;
        _text.text = (int)Time.timeSinceLevelLoad / 60 + ":" + ((int)Time.timeSinceLevelLoad % 60) / 10 + ((int)Time.timeSinceLevelLoad % 60) % 10;
    }

    private void StopTimer()
    {
        _canInrement = false;
    }
}
