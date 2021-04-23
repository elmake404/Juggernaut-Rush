﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastomCollider : MonoBehaviour
{
    [SerializeField]
    private List<InsurmountableObstacle> _insurmountableObstacles = new List<InsurmountableObstacle>();

    [SerializeField]
    private float _radius;

    private void LateUpdate()
    {
        if (_insurmountableObstacles.Count>0)
        {
            for (int i = 0; i < _insurmountableObstacles.Count; i++)
            {
                _insurmountableObstacles[i].TouchTest(transform.position,_radius);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var insurmountable = other.GetComponent<InsurmountableObstacle>();
        if (insurmountable!=null)
        {
            _insurmountableObstacles.Add(insurmountable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var insurmountable = other.GetComponent<InsurmountableObstacle>();
        if (insurmountable!=null)
        {
            _insurmountableObstacles.Remove(insurmountable);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,_radius) ;
    }
}
