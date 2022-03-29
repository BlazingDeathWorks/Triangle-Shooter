using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    public ParticleSystem ps;

    private void Start()
    {
        
    }
    private void OnDestroy()
    {
        Instantiate(ps, this.transform.position, Quaternion.identity);
    }
}
