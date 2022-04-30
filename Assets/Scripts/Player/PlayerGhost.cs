using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    [SerializeField] private float _ghostDuration = 1.5f;
    private NormalPoolableObject _normalPoolableObject;
    private float _timeSinceEnabled = 0;

    private void Awake()
    {
        _normalPoolableObject = GetComponent<NormalPoolableObject>();
    }

    private void OnEnable()
    {
        _timeSinceEnabled = 0;
        if (_normalPoolableObject == null) return;
        while (_timeSinceEnabled < _ghostDuration)
        {
            _timeSinceEnabled += Time.deltaTime;
        }
        Debug.Log(_normalPoolableObject.ParentObjectPooler);
        ObjectPool.Return(_normalPoolableObject);
    }
}
