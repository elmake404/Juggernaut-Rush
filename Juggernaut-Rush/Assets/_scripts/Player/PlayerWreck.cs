using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWreck : MonoBehaviour
{
    [SerializeField]
    private PlayerLife _playerLife;
    [SerializeField]
    private float _impactStrength;

    public delegate void StressTransmitter(float stress);
    public event StressTransmitter OnWreck;

    private void OnCollisionEnter(Collision collision)
    {
        var wreckage = collision.collider.GetComponent<Wreckage>();
        if (wreckage != null)
        {
            OnWreck?.Invoke(wreckage.StressLevel);
            _playerLife.RestoringRage(wreckage.PercentagofRageRecovery);

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
