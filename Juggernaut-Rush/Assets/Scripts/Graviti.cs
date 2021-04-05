using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graviti : MonoBehaviour
{
    
    [SerializeField]
    private Rigidbody _rbMain;
    [SerializeField]
    private Vector3 _directionGraviti;
    private Vector3 _direction;
    private void Start()
    {

        if (_directionGraviti==Vector3.zero)
        {
            _directionGraviti = Vector3.down;
        }

        if (_rbMain==null)
        {
            Debug.Log(name+ " absent Rigidbody");
        }
        DefaultGraviti();

    }

    private void FixedUpdate()
    {
        if (!_rbMain.useGravity)
        {
            _rbMain.AddForce(_directionGraviti * 9.8f, ForceMode.Acceleration);
        }
    }
    public void ReverseGraviti()
    {
        _direction = _directionGraviti *-1;
    }
    public void DefaultGraviti()
    {
        _direction = _directionGraviti;
    }

    public void AddForceGraviti(float forse)
    {
        _rbMain.AddForce(_direction * forse, ForceMode.Acceleration);

    }
    [ContextMenu("GetRigidbody")]
    public void GetRigidbody()
    {
        _rbMain = GetComponent<Rigidbody>();
    }

}
