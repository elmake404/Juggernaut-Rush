using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField]
    private ParticleSystem _particle;
    private PlayerLife _playerLife 
    { get { return PlayerLife.Instance; } }
    [SerializeField]
    private float _radius, _forceExplosion;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.gameObject == PlayerLife.Instance.gameObject)
            {
                if (_playerLife.GetAmoutRage()<_playerLife.PowerOfUnstoppability)
                {
                    _playerLife.Death();
                }
                Detonation();
            }

    }
    private void Detonation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var item in colliders)
        {
            var wall = item.GetComponent<WholeObj>();
            if (wall != null)
            {
                wall.ActivationWallWreckage();
            }
        }
        colliders = Physics.OverlapSphere(transform.position, _radius);
        float addRage = 0;
        foreach (var item in colliders)
        {
            var wreckage = item.GetComponent<Wreckage>();
            if (wreckage != null)
            {
                wreckage.Explosion(_forceExplosion, transform.position, _radius);
                addRage += wreckage.PercentagofRageRecovery;
            }
        }
        PlayerLife.Instance.RestoringRage(addRage);
        _particle.Play();
        _particle.transform.SetParent(null);

        Destroy(gameObject);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_particle.transform.position, _radius);
    }   
}
