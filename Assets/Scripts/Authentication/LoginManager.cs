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

    public void SignUp()
    {
        LootLockerSDKManager.WhiteLabelSignUp(_username?.text, _password?.text, (response) =>
        {
            if (!response.success)
            {
                //Fix this so we cut out everything except for the actual message
                _errorText.text = response.Error;
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
                _errorText.text = response.Error;
                return;
            }

            string token = response.SessionToken;

            // Start game session here
            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                if (!response.success)
                {
                    //Fix this so we cut out everything except for the actual message
                    _errorText.text = response.Error;
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
