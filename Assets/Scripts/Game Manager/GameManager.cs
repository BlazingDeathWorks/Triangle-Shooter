using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class GameManager : MonoBehaviour
{
    [SerializeField] private ActionChannel _gameStartedEventHandler;
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] private GameObject _gameOverTab;

    private void Awake()
    {
        _gameOverTab.SetActive(false);
        _gameStartedEventHandler?.CallAction();
        _playerDiedEventHandler?.AddAction(() => StartCoroutine(GameOver()));
        Debug.Log(_gameOverTab.GetComponentInChildren<Text>().text);
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(2f);
        _gameOverTab.SetActive(true);
    }
}
