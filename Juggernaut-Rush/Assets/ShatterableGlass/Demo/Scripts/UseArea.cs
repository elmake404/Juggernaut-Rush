using UnityEngine;
using System.Collections;

// If Player within triggr, and player pressed E then Glass will shatter.
public class UseArea : MonoBehaviour
{
    // Player in trigger?
    bool PlayerIsHere = false;
    // Target Glass
    public ShatterableGlass Glass;

    void OnTriggerEnter(Collider Intruder)
    {
        if (Intruder.tag == "Player")
            PlayerIsHere = true;
    }

    void OnTriggerExit(Collider Intruder)
    {
        if (Intruder.tag == "Player")
            PlayerIsHere = false;
    }

    // This funtion called when Player pressed E.
    public void Use()
    {
        // Do not attepmt to shatter glass, if Glass already Destroyed().
        if (Glass && PlayerIsHere)
            Glass.Shatter(Vector2.zero, Glass.transform.forward);
    }
}