using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [System.Serializable]
    public struct MinMaxFalot
    {
        public float Min;
        public float Max;
        private float _difference 
        { get { return Max - Min; } }

        public float GetSpeed (float Amout)
        {
            return Min + (_difference*Amout);
        }
    }
    private Vector3 _startTouchPos, _currentPosPlayer, _targetPosPlayer;
    private Camera _cam;
    [SerializeField]
    private PlayerLife _playerLife;

    [SerializeField]
    private MinMaxFalot _lateralSpeed, _runningSpeed;
    private float _amoutRage { get { return _playerLife.GetAmoutRage(); } }

    private void Awake()
    {

    }
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
            transform.position = Vector3.MoveTowards(transform.position, PosX, GetSpeed(_lateralSpeed));

            transform.Translate(Vector3.forward * GetSpeed(_runningSpeed));
        }
    }
    private float GetSpeed(MinMaxFalot Speed) => Speed.GetSpeed(_amoutRage);

}
