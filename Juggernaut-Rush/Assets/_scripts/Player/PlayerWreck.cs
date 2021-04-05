using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWreck : MonoBehaviour
{
    [SerializeField]
    private float _impactStrength;
    private void OnCollisionEnter(Collision collision)
    {
        var wreckage = collision.collider.GetComponent<Wreckage>();
        if (wreckage != null)
        {
            wreckage.PushWreckage((wreckage.transform.position - transform.position).normalized, collision.contacts[0].point, _impactStrength);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var wall = other.GetComponent<WholeObj>();
        if (wall != null)
        {
            wall.ActivationWallWreckage();
        }
    }
}
