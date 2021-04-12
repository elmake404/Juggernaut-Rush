using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var wreckage = other.GetComponent<Wreckage>(); 
        if (wreckage!=null)
        {
            Destroy(wreckage.gameObject);
        }
    }
}
