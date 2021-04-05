using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float _smoothTime;
    private PlayerMove _target;
    private Vector3 _velocity, _offset;

    private void Start()
    {
        _target = FindObjectOfType<PlayerMove>();
        _offset = transform.position - _target.transform.position;
    }

    private void FixedUpdate()
    {
        if (GameStage.IsGameFlowe)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _target.transform.position + _offset, ref _velocity, _smoothTime);
        }
    }
}
