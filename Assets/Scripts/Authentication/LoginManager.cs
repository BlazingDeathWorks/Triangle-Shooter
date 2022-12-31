using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

internal class LoginManager : MonoBehaviour
{
    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;
    [SerializeField] private Text _errorText;
    private const string PLAYER_ID_KEY = "PlayerID";

    [ContextMenu("Clear Player ID")]
    private void ClearPlayerID()
    {
        PlayerPrefs.DeleteKey(PLAYER_ID_KEY);
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
                PlayerPrefs.SetInt(PLAYER_ID_KEY, response.player_id);
                SceneController.Instance.NextScene();
            });
        });
    }
}
