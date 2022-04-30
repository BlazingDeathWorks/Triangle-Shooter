using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerGhost : MonoBehaviour
{
    [SerializeField] private float _ghostDuration = 1.5f;
    [SerializeField] private NormalPoolableObject _normalPoolableObject;
    private float _timeSinceEnabled = 0;

    private void Update()
    {
        if (_normalPoolableObject == null) return;
        if (_timeSinceEnabled < _ghostDuration)
        {
            _timeSinceEnabled += Time.deltaTime;
            return;
        }
        _timeSinceEnabled = 0;
        ObjectPool.Return(_normalPoolableObject);
    }
}
