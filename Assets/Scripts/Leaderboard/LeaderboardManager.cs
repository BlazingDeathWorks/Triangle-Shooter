using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    private static int s_leaderboardId => 10158;
    private static int s_playerId => PlayerPrefs.GetInt(LoginManager.PlayerIdKey);

    public static void SubmitScore(int score)
    {
        LootLockerSDKManager.SubmitScore(s_playerId.ToString(), score, s_leaderboardId.ToString(), (response) =>
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
