using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferenceManager : MonoBehaviour
{
    [SerializeField] private string[] _keys;
    [SerializeField] private GameObject[] _values;

    private static Dictionary<string, GameObject> _sceneReferences = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (_keys.Length != _values.Length) return;
        for (int i = 0; i < _keys.Length; i++)
        {
            _sceneReferences.Add(_keys[i], _values[i]);
        }
    }

    public static GameObject GetReference(string key)
    {
        if (!_sceneReferences.ContainsKey(key)) return null;
        return _sceneReferences[key];
    }
}
