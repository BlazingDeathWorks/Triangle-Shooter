using UnityEngine;
using UnityEngine.UI;

internal class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    public Text Text => _text;
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
}
