using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wreckage : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rbWreckage;
    [SerializeField]
    private FixedJoint _fixedJoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rbWreckage.isKinematic = !_rbWreckage.isKinematic;
        }
    }
    public void PushWreckage(Vector3 direction,Vector3 contactPoint,float forse)
    {
        Destroy(_fixedJoint);
        _rbWreckage.AddForceAtPosition(direction*forse, contactPoint, ForceMode.Acceleration);
        enabled = false;
    }
    [ContextMenu("Initialization")]
    private void Initialization()
    {
        _rbWreckage = GetComponent<Rigidbody>();
        _fixedJoint = GetComponent<FixedJoint>();
    }
}
