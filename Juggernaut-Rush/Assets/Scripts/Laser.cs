using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;
    private void Start()
    {
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.forward * -15);
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.tag == "Wall")
            {
                _lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
            }
            else
            {
                _lineRenderer.SetPosition(1, Vector3.forward * -15);
            }
        }
    }
}
