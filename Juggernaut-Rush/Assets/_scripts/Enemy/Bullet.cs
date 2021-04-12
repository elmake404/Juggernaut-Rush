using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BulletCharacteristics
{
    public float FlightSpeed, TimeLife , DamagePercentage;
}

public class Bullet : MonoBehaviour
{
    private BulletCharacteristics _characteristics;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _characteristics.FlightSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent!=null)
        if (other.transform.parent.gameObject == PlayerLife.Instance.gameObject )
        {
            PlayerLife.Instance.RestoringRage(_characteristics.DamagePercentage);
            Destroy(gameObject);
        }
    }
    public void Initialization(BulletCharacteristics bullet)
    {
        _characteristics = bullet;
        Destroy(gameObject,bullet.TimeLife);
    }
}
