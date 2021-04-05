using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormal : MonoBehaviour
{
    private RaycastHit hitNear;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Player _player;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = _player.transform.position;
        Physics.Raycast(transform.position , Vector3.left, out hitNear, 2.0f, layerMask);
        //Normal Rotation
        _player.transform.up = Vector3.Lerp(_player.transform.up, hitNear.normal, 0.5f);
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }
}
