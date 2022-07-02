using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostContainerManager : MonoBehaviour
{
    [SerializeField] private Transform[] _containers;
    [SerializeField] private string[] _containerNames;
    private static Dictionary<string, Transform> s_containerDictionary = new Dictionary<string, Transform>();

    private void Awake()
    {
        if (_containerNames.Length != _containers.Length) return;
        for (int i = 0; i < _containers.Length; i++)
        {
            s_containerDictionary.Add(_containerNames[i], _containers[i]);
        }
    }

    private void OnDestroy()
    {
        s_containerDictionary.Clear();
    }

    public static Transform GetValue(string key)
    {
        if (!s_containerDictionary.ContainsKey(key)) return null;
        return s_containerDictionary[key];
    }
}
