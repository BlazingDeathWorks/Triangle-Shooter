using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeTween : MonoBehaviour
{
    [SerializeField] private float _from, _to, _shakeTime;
    private CinemachineBasicMultiChannelPerlin _multiPerlin;

    private void Awake()
    {
        _multiPerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _multiPerlin.m_AmplitudeGain = _from;
    }

    public void Shake()
    {
        if (_multiPerlin == null) return;

        LeanTween.value(gameObject, _from, _to, _shakeTime)
        .setOnUpdate((value) =>
        {
            _multiPerlin.m_AmplitudeGain = Mathf.Lerp(_multiPerlin.m_AmplitudeGain, _to, value);
        })
        .setOnComplete(() =>
        {
            LeanTween.value(gameObject, _from, _to, _shakeTime)
            .setOnUpdate((value) =>
            {
                _multiPerlin.m_AmplitudeGain = Mathf.Lerp(_multiPerlin.m_AmplitudeGain, _from, value);
            });
        });

    }
}
