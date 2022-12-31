using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private int _count = 3;
    private static string s_leaderboardId => "10158";
    private static string s_playerId => PlayerPrefs.GetInt(LoginManager.PlayerIdKey).ToString();

    private void Awake()
    {
        LootLockerSDKManager.GetScoreList(s_leaderboardId, _count, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successfully Loaded Score List");

                return;
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
}
