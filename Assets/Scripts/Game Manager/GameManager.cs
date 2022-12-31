using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

internal sealed class GameManager : MonoBehaviour
{
    [SerializeField] private ActionChannel _gameStartedEventHandler;
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] private GameObject _gameOverTab;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _scoreText;
    private int _currentScore;

    private void Awake()
    {
        _gameOverTab.SetActive(false);
        _gameStartedEventHandler?.CallAction();
        _playerDiedEventHandler?.AddAction(StartGameOver);
    }

    private void OnDestroy()
    {
        _playerDiedEventHandler?.RemoveAction(StartGameOver);
    }

    private void StartGameOver() => StartCoroutine(GameOver());

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        UpdateScoreText();
        LoadBestScore();
        _gameOverTab.SetActive(true);
    }

    private void UpdateScoreText()
    {
        _scoreText.text = $"SCORE: {ReturnScore()}";
    }

    private int ReturnScore()
    {
        _currentScore = Timer.Instance.CalculateScore() + KillCounter.Instance.CalculateScore();
        return _currentScore;
    }

    private void LoadBestScore()
    {
        int highScore = LeaderboardManager.GetHighscore();

        if (highScore < _currentScore)
        {
            LeaderboardManager.SubmitScore(_currentScore);
            _bestScoreText.text = $"BEST SCORE: {_currentScore}";
            return;
        }

        _bestScoreText.text = $"BEST SCORE: {highScore}";
    }
}
