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
    private string _path;
    private int _currentScore;

    private void Awake()
    {
        _path = Application.persistentDataPath + FILE_NAME;
        _gameOverTab.SetActive(false);
        _gameStartedEventHandler?.CallAction();
        _playerDiedEventHandler?.AddAction(StartGameOver);
        //ClearBestScore();
    }

    private void OnDestroy()
    {
        _playerDiedEventHandler?.RemoveAction(StartGameOver);
    }

    private void StartGameOver() => StartCoroutine(GameOver());

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
        _currentScore = Timer.Instance.CalculateScore() + KillCounter.Instance.CalculateScore();
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
