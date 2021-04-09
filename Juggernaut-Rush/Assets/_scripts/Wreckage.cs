using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wreckage : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rbWreckage;
    [SerializeField]
    private FixedJoint _fixedJoint;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
    public void PushWreckage(Vector3 direction,Vector3 contactPoint,float forse)
    {
        Destroy(_fixedJoint);
        transform.SetParent(null);
        _rbWreckage.AddForceAtPosition(direction*forse, contactPoint, ForceMode.Acceleration);
        enabled = false;
        Destroy(gameObject,2);
    }
    [ContextMenu("Initialization")]
    private void Initialization()
    {
        _rbWreckage = GetComponent<Rigidbody>();
        _fixedJoint = GetComponent<FixedJoint>();
    }
}
