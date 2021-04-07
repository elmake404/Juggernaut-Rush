using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BulletCharacteristics
{
    public float FlightSpeed, TimeLife;
}

public class Bullet : MonoBehaviour
{
    private float _flightSpeed, _timeLife;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _flightSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMove>();
        if (player!=null)
        {
            Destroy(gameObject);
        }
    }
    public void Initialization(BulletCharacteristics bullet)
    {
        _flightSpeed = bullet.FlightSpeed;
        _timeLife = bullet.TimeLife;
        Destroy(gameObject,_timeLife);
    }
}
