using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using System;

public class LoginManager : MonoBehaviour
{
    public static Action LoggedIn { get; set; }
    public static string PlayerIdKey { get; private set; } = "PlayerID";
    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;
    [SerializeField] private Text _errorText;

    [ContextMenu("Clear Player ID")]
    private void ClearPlayerID()
    {
        PlayerPrefs.DeleteKey(PlayerIdKey);
    }

    private string DisplayErrorMessage(string error)
    {
        if (string.IsNullOrEmpty(error)) return "Error logging in\nTry restarting";

        const string MESSAGE = "message";
        int startingIndex = error.IndexOf(MESSAGE) + MESSAGE.Length + 3;
        int endingIndex = error.Substring(startingIndex).IndexOf("\"");
        return char.ToUpper(error[startingIndex]).ToString() + error.Substring(startingIndex + 1, endingIndex - 1);
    }

    public void SignUp()
    {
        LootLockerSDKManager.WhiteLabelSignUp(_username?.text, _password?.text, (response) =>
        {
            if (!response.success)
            {
                //Fix this so we cut out everything except for the actual message
                _errorText.text = DisplayErrorMessage(response.Error);
                return;
            }

            Login();
        });
    }

    public void Login()
    {
        LootLockerSDKManager.WhiteLabelLogin(_username.text, _password.text, response =>
        {
            if (!response.success)
            {
                //Fix this so we cut out everything except for the actual message
                _errorText.text = DisplayErrorMessage(response.Error);
                return;
            }

            string token = response.SessionToken;

            // Start game session here
            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                if (!response.success)
                {
                    //Fix this so we cut out everything except for the actual message
                    _errorText.text = DisplayErrorMessage(response.Error);
                    return;
                }

                LootLockerSDKManager.GetPlayerName((response) =>
                {
                    if (!response.success) return;
                    if (string.IsNullOrEmpty(response.name))
                    {
                        LootLockerSDKManager.SetPlayerName(_username.text, (response) => { });
                    }
                });
                PlayerPrefs.SetInt(PlayerIdKey, response.player_id);
                LoggedIn?.Invoke();
                SceneController.Instance.NextScene();
            });
        });
    }
}
