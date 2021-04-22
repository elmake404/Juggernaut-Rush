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

        public float GetSpeed(float Amout)
        {
            return Min + (_difference * Amout);
        }
    }
    private Vector3 _startTouchPos, _currentPosPlayer, _targetPosPlayer,_startPosPlayer;
    private List<float> _ListPositivBost = new List<float>();
    private List<float> _ListNegativBost = new List<float>();
    private Camera _cam;
    [SerializeField]
    private PlayerLife _playerLife;
    [SerializeField]
    private float _lateralSpeed, _runningSpeed, _boosterAccelerationPercentage;
    [SerializeField]
    private float _horizontalLimit;

    private void Start()
    {
        _startPosPlayer = transform.position;
        _targetPosPlayer = transform.position;
        _cam = Camera.main;
    }
    private void Update()
    {
        if (TouchUtility.TouchCount > 0 && PossibleToRun())
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
        if (PossibleToRun())
        {
            Vector3 PosX = transform.position;
            PosX.x = CheckLimmit(_targetPosPlayer);
            transform.position = Vector3.MoveTowards(transform.position, PosX, GetSpeed(_lateralSpeed));

            transform.Translate(Vector3.forward * GetSpeed(_runningSpeed));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent!=null)
        {
            SpeedInspector inspector = other.transform.parent.GetComponent<SpeedInspector>();
            if (inspector != null)
            {
                inspector.Act();
                StartCoroutine(Buff(inspector.Bonus, inspector.TimeOfAction));
            }
        }
    }
    private float CheckLimmit(Vector3 target)
    {
        if (target.x >_startPosPlayer.x+_horizontalLimit)
        {
            target.x = _startPosPlayer.x + _horizontalLimit;
        }
        else if (target.x <_startPosPlayer.x-_horizontalLimit)
        {
            target.x = _startPosPlayer.x - _horizontalLimit;
        }
        return target.x;
    }
    private IEnumerator Buff(float Procent,float time)
    {
        if (Procent>0)
        {
            _ListPositivBost.Add(Procent);
            yield return new WaitForSeconds(time);
            _ListPositivBost.Remove(Procent);
        }
        else
        {
            _ListNegativBost.Add(Procent);
            yield return new WaitForSeconds(time);
            _ListNegativBost.Remove(Procent);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position- Vector3.right*_horizontalLimit,transform.position+Vector3.right*_horizontalLimit);
    }
    private float GetSpeed(float Speed)
    {
        float negative = Speed;
        for (int i = 0; i < _ListNegativBost.Count; i++)
        {
            negative += (negative / 100) * _ListNegativBost[i];
        }
        negative -= Speed;

        float positive = Speed;
        for (int i = 0; i < _ListPositivBost.Count; i++)
        {
            positive += (positive / 100) * _ListPositivBost[i];
        }
        positive -= Speed;
        float addSpeed = _playerLife.IsBoostActivation ? (Speed / 100) * _boosterAccelerationPercentage:0;
        return Speed+(negative+positive)+addSpeed;
    }
    public bool PossibleToRun() => (PlayerLife.IsGetAngry && GameStage.IsGameFlowe);
    public float GetAmoutSpeed()
    {
        float negative = 1;
        for (int i = 0; i < _ListNegativBost.Count; i++)
        {
            negative += (negative / 100) * _ListNegativBost[i];
        }
        negative -= 1;

        float positive = 1;
        for (int i = 0; i < _ListPositivBost.Count; i++)
        {
            positive += (positive / 100) * _ListPositivBost[i];
        }
        positive -= 1;
        float addSpeed = _playerLife.IsBoostActivation ? (1f/ 100f) * _boosterAccelerationPercentage : 0;
        return 1 + (negative + positive) + addSpeed;

    }
    
    
}
