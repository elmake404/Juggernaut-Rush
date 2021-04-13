using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wreck : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    private float _stressLevel; public float StressLevel
    {
        get
        {
            return _stressLevel;
        }
    }

    public void PlayParticle(Vector3 posParticle, Vector3 forvard)
    {
        _particle.transform.position = posParticle;
        _particle.transform.forward = forvard;
        _particle.Play();
    }
}
