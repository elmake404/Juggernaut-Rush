using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsurmountableObstacle : MonoBehaviour
{
    [SerializeField]
    private float _radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void TouchTest(Vector3 position,Vector3 extremePointPosition)
    {
        //position =
    }

}
