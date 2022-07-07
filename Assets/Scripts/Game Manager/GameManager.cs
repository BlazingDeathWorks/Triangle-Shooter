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
    private const string FILE_NAME = "/Best Score.bin";
    private const int SCORE_MULTIPLY = 5;
    private string _path;
    private int _currentScore;

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
        //times will always be of a length of 2
        string[] times = Timer.Instance.Text.text.Split(':');
        try
        {
            int seconds = ((int.Parse(times[0]) * 60) + int.Parse(times[1])) * SCORE_MULTIPLY;
            _currentScore = seconds + (KillCounter.Instance.KillCount * SCORE_MULTIPLY);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        return _currentScore;
    }

    private void LoadBestScore()
    {
        ScoreBoard highScore = BinarySaveSystem.LoadSystem<ScoreBoard>(_path);

        //Should do basically the same thing as if current score was greater
        if (highScore == null)
        {
            highScore = new ScoreBoard(_currentScore);
            BinarySaveSystem.SaveSystem(highScore, _path);
        }

        //Update Best Score with Current Score to system: highScore == null || highScore.Score < currentScore
        if (highScore.Score < _currentScore)
        {
            highScore.Score = _currentScore;
            BinarySaveSystem.SaveSystem(highScore, _path);
        }

        _bestScoreText.text = $"BEST SCORE: {highScore.Score}";
    }

    /*private void ClearBestScore()
    {
        ScoreBoard highScore = BinarySaveSystem.LoadSystem<ScoreBoard>(_path);
        highScore.Score = 0;
        BinarySaveSystem.SaveSystem(highScore, _path);
    }*/
}

[Serializable]
public class ScoreBoard
{
    public int Score;

    public ScoreBoard(int score)
    {
        Score = score;
    }
}
