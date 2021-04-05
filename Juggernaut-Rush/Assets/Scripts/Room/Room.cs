using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other == Player.PlayerMain.GetFeet())
        {
            var graviti = other.GetComponentInParent<Graviti>();
            graviti.AddForceGraviti(25);
            graviti.ReverseGraviti();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == Player.PlayerMain.GetFeet())
        {
            other.GetComponentInParent<Graviti>().DefaultGraviti();
        }
    }
}
