using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
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

            Debug.Log("user created successfully");
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

                Debug.Log("session started successfully");
                PlayerPrefs.SetInt(PlayerIdKey, response.player_id);
                SceneController.Instance.NextScene();
            });
        });
    }
}
