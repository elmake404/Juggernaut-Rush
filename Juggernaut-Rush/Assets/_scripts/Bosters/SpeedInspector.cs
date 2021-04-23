using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedInspector : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    private float _bonus, _timeOfAction, _percentagofRageRecovery; 
    public float PercentagofRageRecovery
    { get { return _percentagofRageRecovery; } }

    public float Bonus { get { return _bonus; } }
    public float TimeOfAction { get { return _timeOfAction; } }

    public void Act()
    {
        if (_animator != null)
            _animator.SetBool("Act", true);
        if (_particle != null)
            _particle.Play();

        Destroy(this);
    }
}
