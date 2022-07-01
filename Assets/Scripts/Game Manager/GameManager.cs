using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class GameManager : MonoBehaviour
{
    [SerializeField] private ActionChannel _gameStartedEventHandler;

    private void Awake()
    {
        _gameStartedEventHandler?.CallAction();
    }
}
