using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 _startTouchPos, _currentPosPlayer, _targetPosPlayer;
    private Camera _cam;
    [SerializeField]
    private Rigidbody _rbMain;

    [SerializeField]
    private float _lateralSpeed, _runningSpeed;

    void Start()
    {
        _targetPosPlayer = transform.position;
        _cam = Camera.main;
    }
    private void Update()
    {
        if (TouchUtility.TouchCount > 0 && GameStage.IsGameFlowe)
        {
            Touch touch = TouchUtility.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                _currentPosPlayer = transform.position;
                _startTouchPos = (_cam.transform.position - ((ray.direction) *
                        ((_cam.transform.position - transform.position).z / ray.direction.z)));
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

                if (_startTouchPos == Vector3.zero)
                {
                    _startTouchPos = (_cam.transform.position - ((ray.direction) *
                            ((_cam.transform.position - transform.position).z / ray.direction.z)));
                }

                _targetPosPlayer = _currentPosPlayer + ((_cam.transform.position - ((ray.direction) *
                        ((_cam.transform.position - transform.position).z / ray.direction.z))) - _startTouchPos);
            }
        }
        else
        {
            _targetPosPlayer = transform.position;
        }

    }
    private void FixedUpdate()
    {
        if (GameStage.IsGameFlowe)
        {
            Vector3 PosX = transform.position;
            PosX.x = _targetPosPlayer.x;
            transform.position = Vector3.MoveTowards(transform.position, PosX, _lateralSpeed);

            transform.Translate(Vector3.forward * _runningSpeed);
        }
    }
    public void GameOver()
    {
        _rbMain.constraints = RigidbodyConstraints.FreezeRotation;
        _rbMain.AddForce(Vector3.forward*500,ForceMode.Acceleration);
       transform.GetChild(0).gameObject.layer=8;
        
        GameStage.Instance.ChangeStage(Stage.LostGame);
    }

}
