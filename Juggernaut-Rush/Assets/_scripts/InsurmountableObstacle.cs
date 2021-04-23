using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsurmountableObstacle : MonoBehaviour
{
    [SerializeField]
    private float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void TouchTest(Vector3 position, float radius)
    {
        position.y = 0;
        Vector3 posObj = transform.position;
        posObj.y = 0;

        Vector3 ExtremePointPositionTarget = position + GetExtremePointPosition(position, posObj, radius);
        Vector3 ExtremePointPositionObj = posObj + GetExtremePointPosition(posObj, position, _radius);

        if (TouchCheck(transform.InverseTransformPoint(ExtremePointPositionTarget), transform.InverseTransformPoint(ExtremePointPositionObj)))
        {
            Debug.Log(1234);
        }
        //Debug.Log(transform.InverseTransformPoint(ExtremePointPositionTarget));
        //Debug.Log(transform.InverseTransformPoint(ExtremePointPositionObj));

    }

    private Vector3 GetExtremePointPosition(Vector3 posReference, Vector3 posTarget, float radius)
    {
        Vector3 ExtremePoint = posTarget - posReference;
        return ExtremePoint.normalized * radius;
    }
    private bool TouchCheck(Vector3 positiontarget,Vector3 positionObj)
    {
        if (Mathf.Abs(positionObj.x)<Mathf.Abs(positiontarget.x))
        {
            return true;
        }
        else if (Mathf.Abs(positionObj.y) < Mathf.Abs(positiontarget.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
