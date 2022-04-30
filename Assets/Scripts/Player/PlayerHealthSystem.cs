using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem Instance { get; private set; }

    [SerializeField] ActionChannel _playerDiedEventHandler;
    [SerializeField] ActionChannel _playerCollidedEventHandler;

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
