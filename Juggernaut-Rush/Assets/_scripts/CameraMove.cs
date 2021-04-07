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
        _target = PlayerLife.Instance.GetComponent<PlayerMove>();
        _offset = transform.position - _target.transform.position;
    }

    private void FixedUpdate()
    {
        if (GameStage.IsGameFlowe)
        {
            Vector3 NextPosCamera = (_target.transform.position + _offset);
            NextPosCamera.x = transform.position.x;
            transform.position = Vector3.SmoothDamp(transform.position, NextPosCamera, ref _velocity, _smoothTime);
        }
    }
}
