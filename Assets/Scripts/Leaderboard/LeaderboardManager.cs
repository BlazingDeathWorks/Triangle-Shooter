using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private bool _loginException = false;
    [SerializeField] private int _count = 3;
    private static string s_leaderboardId => "10158";
    private static string s_playerId => PlayerPrefs.GetInt(LoginManager.PlayerIdKey).ToString();
    private static int s_highscore = 0;

    private void Awake()
    {
        if (_loginException)
        {
            LoginManager.LoggedIn += SetHighscore;
            return;
        }
        LootLockerSDKManager.GetScoreList(s_leaderboardId, _count, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successfully Loaded Score List");
                Debug.Log(response.items[0].score);
                Debug.Log(response.items.Length);
                return;
            }
        });
    }

    private void SetHighscore()
    {
        LootLockerSDKManager.GetMemberRank(s_leaderboardId, s_playerId, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
                Debug.Log(response.score);
                s_highscore = response.score;
            }
        });
    }

    public static void SubmitScore(int score)
    {
        LootLockerSDKManager.SubmitScore(s_playerId, score, s_leaderboardId, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
                return;
            }
            //Failed if I didn't log in most likely so we do nothing because this only happens when playtesting
        });
    }

    public static int GetHighscore()
    {
        return s_highscore;
    }
}
