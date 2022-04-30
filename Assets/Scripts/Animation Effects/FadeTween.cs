using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTween : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 0.25f;
    private Color _beginColor, _endColor;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _beginColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, _sr.color.a);
        _endColor = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 0);
    }

    private void OnEnable()
    {
        LeanTween.value(gameObject, 0, 1, _fadeTime)
        .setOnUpdate((value) =>
        {
            _sr.color = Color.Lerp(_beginColor, _endColor, value);
        });
    }
}
