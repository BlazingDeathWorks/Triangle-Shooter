using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    [SerializeField] private Vector3 _to;
    [SerializeField] private float _scaleTime;
    private Vector3 _originalPos;

    private void Awake()
    {
        _originalPos = transform.localScale;
    }

    public void ScaleObject()
    {
        LeanTween.scale(gameObject, _to, _scaleTime).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, _originalPos, _scaleTime);
        });
    }
}
