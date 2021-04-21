using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField]
    private float _percentagofRageRecovery;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.gameObject == PlayerLife.Instance.gameObject)
            {
                PlayerLife.Instance.RestoringRage(_percentagofRageRecovery);
                Destroy(gameObject);
            }
    }
}

