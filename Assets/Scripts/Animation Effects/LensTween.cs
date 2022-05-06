using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensTween : MonoBehaviour
{
    [SerializeField] private float _from, _to;
    [SerializeField] private float _distortTime;
    private bool _isPlaying = false;
    private Volume _volume;
    private LensDistortion _lensDistortion;

    private void Awake()
    {
        _volume = GetComponent<Volume>();
        if (_volume == null) return;
        _volume.profile.TryGet(out _lensDistortion);
    }

    public void DistortLens()
    {
        if (_lensDistortion == null || _isPlaying) return;

        _isPlaying = true;
        _lensDistortion.intensity.value = _from;
        LeanTween.value(gameObject, _from, _to, _distortTime)
        .setOnUpdate((value) =>
        {
            _lensDistortion.intensity.value = Mathf.Lerp(_lensDistortion.intensity.value, _to, value);
        })
        .setOnComplete(() =>
        {
            LeanTween.value(gameObject, _from, _to, _distortTime)
            .setOnUpdate((value) =>
            {
                _lensDistortion.intensity.value = Mathf.Lerp(_lensDistortion.intensity.value, _from, value);
            })
            .setOnComplete(() =>
            {
                _isPlaying = false;
            });
        });

    }
}
