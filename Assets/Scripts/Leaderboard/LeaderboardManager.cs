using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private bool _loginException = false;
    [SerializeField] private int _count = 3;
    [SerializeField] private LeaderboardItem[] _leaderboardItems;
    private static string s_leaderboardKey => "highscore_leaderboard";
    private static string s_playerId => PlayerPrefs.GetInt(LoginManager.PlayerIdKey).ToString();
    private static int s_highscore = 0;

    private void Awake()
    {
        if (_loginException)
        {
            LoginManager.LoggedIn += SetHighscore;
            return;
        }

        if (_count != _leaderboardItems.Length) return;
        LootLockerSDKManager.GetScoreList(s_leaderboardKey, _count, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                for (int i = 0; i < _count; i++)
                {
                    if (response.items.Length - 1 < i) return;
                    _leaderboardItems[i].UsernameText.text = response.items[i].player.name;
                    _leaderboardItems[i].ScoreText.text = response.items[i].score.ToString();
                }
            }
        });
    }

    private void SetHighscore()
    {
        LootLockerSDKManager.GetMemberRank(s_leaderboardKey, s_playerId, (response) =>
        {
            if (response.statusCode == 200)
            {
                s_highscore = response.score;
            }
        });
    }

    public static void SubmitScore(int score)
    {
        LootLockerSDKManager.SubmitScore(s_playerId, score, s_leaderboardKey, (response) =>
        {
            /*if (response.statusCode == 200)
            {
                Debug.Log("Successful");
            }*/
            //Failed if I didn't log in most likely so we do nothing because this only happens when playtesting
        });
    }

    public static int GetHighscore()
    {
        return s_highscore;
    }
}

[System.Serializable]
internal class LeaderboardItem
{
    public Text UsernameText => _usernameText;
    public Text ScoreText => _scoreText;

    [SerializeField] private Text _usernameText;
    [SerializeField] private Text _scoreText;
} 
