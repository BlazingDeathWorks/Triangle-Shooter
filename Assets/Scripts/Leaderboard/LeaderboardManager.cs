using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    public static int LeaderboardId => 10158;
    public static int PlayerId => PlayerPrefs.GetInt(LoginManager.PlayerIdKey);

    public static void Test()
    {
        Debug.Log("hleell");
    }
}
