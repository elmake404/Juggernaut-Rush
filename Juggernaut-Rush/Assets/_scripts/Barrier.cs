using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    private float _radius, _forceExplosion;
    private float _speed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.gameObject == PlayerLife.Instance.gameObject)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
                foreach (var item in colliders)
                {
                    var wall = item.GetComponent<WholeObj>();
                    if (wall!=null)
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
                //Destroy(_particle, 1);

                Destroy(gameObject);

            }

    }
    private IEnumerator MovingBarrile()
    {
        while (true)
        {
            transform.Translate(_direction*_speed);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_particle.transform.position, _radius);
    }   
    public void StartMoving(float speed,Vector3 direction)
    {
        _speed = speed;
        _direction = direction;
        StartCoroutine(MovingBarrile());
    }

}
