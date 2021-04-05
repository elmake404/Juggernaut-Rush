using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField]
    private float _speedMove;
    void Start()
    {
        Destroy(gameObject, 2);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speedMove);
    }
}
