using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Glass will be shattered when Player steps in the trigger.
public class Trigger : MonoBehaviour
{

    // Target Glass
    public ShatterableGlass Glass;

    void OnTriggerEnter(Collider Intruder)
    {
        // Check if Intruder is Player:
        if (Intruder.tag == "Player")
        {
            // Do not attepmt to shatter glass, if Glass already Destroyed().
            if (Glass)
                Glass.Shatter(Vector2.zero, Glass.transform.forward);
            // Destroy() trigger itself.
            Destroy(gameObject);
        }
    }
}
