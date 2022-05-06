using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class FrameRateManager : MonoBehaviour
{
    public static FrameRateManager Instance { get; private set; }
    [SerializeField] private int _startingTargetFrameRate = 60;

    private void Awake()
    {
        #region Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion    

        Application.targetFrameRate = _startingTargetFrameRate;
    }
}
