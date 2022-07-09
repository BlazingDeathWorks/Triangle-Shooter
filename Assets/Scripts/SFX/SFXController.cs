using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SFXController : MonoBehaviour
{
    [SerializeField] private ActionChannel _channel;
    [SerializeField] private AudioClip _clip;
    private AudioSource _source;

    private void Awake()
    {
        _channel?.AddAction(PlayClip);
    }

    private void Start()
    {
        _source = SceneReferenceManager.GetReference("Camera").GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        _channel?.RemoveAction(PlayClip);
    }

    private void PlayClip()
    {
        _source?.PlayOneShot(_clip);
    }
}
