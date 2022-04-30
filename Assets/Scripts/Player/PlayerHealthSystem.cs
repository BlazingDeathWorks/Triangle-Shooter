using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem Instance { get; private set; }

    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] private ActionChannel _playerCollidedEventHandler;
    [SerializeField] private Image _slider;

    private void Awake()
    {
        #region Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion

        _playerDiedEventHandler?.AddAction(() => Destroy(gameObject));
    }

    public void CheckHealth()
    {
        _playerCollidedEventHandler.CallAction();
        if (true)
        {
            _playerDiedEventHandler.CallAction();
        }
    }
}
