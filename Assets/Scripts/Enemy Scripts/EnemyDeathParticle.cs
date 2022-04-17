using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticle : MonoBehaviour, IObjectPoolable<EnemyDeathParticle>
{
    public IObjectPooler<EnemyDeathParticle> ParentObjectPooler { get; set; }
    private EnemyDeathParticle _instance;

    private void Awake()
    {
        _instance = GetComponent<EnemyDeathParticle>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ObjectPool.Return(this);
    }

    public void OnReturn()
    {
        gameObject.SetActive(false);
    }

    public EnemyDeathParticle ReturnComponent()
    {
        return _instance;
    }
}
