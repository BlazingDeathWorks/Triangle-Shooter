using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPoolableObject : MonoBehaviour, IObjectPoolable<NormalPoolableObject>
{
    public IObjectPooler<NormalPoolableObject> ParentObjectPooler { get; set; }
    private NormalPoolableObject _instance = null;

    private void Awake() => _instance = this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ObjectPool.Return(this);
        }
    }

    public void OnReturn() => gameObject.SetActive(false);

    public NormalPoolableObject ReturnComponent() => _instance;
}
