using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private Camera _cam;
    void Start()
    {
        _cam = Camera.main;
    }

    void LateUpdate()
    {
        //Vector3 posLook = new Vector3(transform.position.x, _cam.transform.position.y,transform.position.z);
        Vector3 posLook = new Vector3(transform.position.x, _cam.transform.position.y,_cam.transform.position.z);
        transform.LookAt(posLook);
    }
}
