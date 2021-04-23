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

    public Vector3 TouchTest(Vector3 position, float radius)
    {
        position.y = 0;
        Vector3 posObj = transform.position;
        posObj.y = 0;

        Vector3 ExtremePointPositionTarget = transform.InverseTransformPoint(position + GetExtremePointPosition(position, posObj, radius));
        Vector3 ExtremePointPositionObj = transform.InverseTransformPoint(posObj + GetExtremePointPosition(posObj, position, _radius));

        if (TouchCheck(ExtremePointPositionTarget, ExtremePointPositionObj))
        {
            return (ExtremePointPositionTarget - ExtremePointPositionObj);
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 GetExtremePointPosition(Vector3 posReference, Vector3 posTarget, float radius)
    {
        Vector3 ExtremePoint = posTarget - posReference;
        return ExtremePoint.normalized * radius;
    }
    private bool TouchCheck(Vector3 positiontarget, Vector3 positionObj)
    {
        if (Mathf.Abs(positionObj.x) > Mathf.Abs(positiontarget.x))
        {
            return true;
        }
        else if (Mathf.Abs(positionObj.y) > Mathf.Abs(positiontarget.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
