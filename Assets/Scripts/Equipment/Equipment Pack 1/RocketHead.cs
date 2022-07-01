using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class RocketHead : MonoBehaviour, IObjectPoolable<RocketHead>
{
    public IObjectPooler<RocketHead> ParentObjectPooler { get; set; }
    private ScaleTween _scaleTween;

    private void Awake()
    {
        _scaleTween = GetComponent<ScaleTween>();
    }

    private void OnEnable()
    {
        _scaleTween.ScaleObject();
    }

    public void OnReturn()
    {
        gameObject.SetActive(false);
    }

    public RocketHead ReturnComponent()
    {
        return this;
    }
}
