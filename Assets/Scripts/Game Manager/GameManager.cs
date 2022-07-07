using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal sealed class GameManager : MonoBehaviour
{
    [SerializeField] private ActionChannel _gameStartedEventHandler;
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] private GameObject _gameOverTab;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _scoreText;
    private const string FILE_NAME = "/Best Score.bin";
    private string _path;

    private void Awake()
    {
        _path = Application.persistentDataPath + FILE_NAME;
        _gameOverTab.SetActive(false);
        _gameStartedEventHandler?.CallAction();
        _playerDiedEventHandler?.AddAction(() => StartCoroutine(GameOver()));
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(2f);
        //LoadBestScore();
        _gameOverTab.SetActive(true);
    }

    private void LoadBestScore()
    {
        ScoreBoard highScore = BinarySaveSystem.LoadSystem<ScoreBoard>(_path);
        int score = 0;

        //Update Best Score with Current Score to system: highScore == null || highScore.Score < currentScore
        if (highScore == null)
        {
            
        }

        //No need for updating score to system
        else
        {
            score = highScore.Score;
        }

        _bestScoreText.text = $"BEST SCORE: {score}";
    }

    private class ScoreBoard
    {
        public int Score;

        public ScoreBoard(int score)
        {
            Score = score;
        }
    } 
}
