using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticle : MonoBehaviour, IObjectPoolable<EnemyDeathParticle>
{
    public IObjectPooler<EnemyDeathParticle> ParentObjectPooler { get; set; }
    private EnemyDeathParticle _instance;
    private ParticleSystem.MainModule _mainModule;
    private ParticleSystem.ColorOverLifetimeModule _colorOverLifeModule;

    private void Awake()
    {
        _instance = GetComponent<EnemyDeathParticle>();
        ParticleSystem particleSystem = _instance.GetComponent<ParticleSystem>();
        _mainModule = particleSystem.main;
        _colorOverLifeModule = particleSystem.colorOverLifetime;
    }

    private void OnEnable()
    {
        StartCoroutine(DeactivateParticle());
    }

    private IEnumerator DeactivateParticle()
    {
        yield return new WaitForSecondsRealtime(1.5f);
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

    public ParticleSystem.MainModule ReturnMainModule()
    {
        return _mainModule;
    }

    public ParticleSystem.ColorOverLifetimeModule ReturnColorOverLifeModule()
    {
        return _colorOverLifeModule;
    }
}
