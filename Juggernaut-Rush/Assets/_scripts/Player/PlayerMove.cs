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
    private Vector3 _startTouchPos, _currentPosPlayer, _targetPosPlayer;
    private Camera _cam;
    [SerializeField]
    private PlayerLife _playerLife;
    [SerializeField]
    private MinMaxFalot _lateralSpeed, _runningSpeed;
    private List<float> _ListPositivBost = new List<float>();
    private List<float> _ListNegativBost = new List<float>();

    private float _amoutRage { get { return _playerLife.GetAmoutRage(); } }

    private void Awake()
    {

    }
    private void Start()
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
    private float GetSpeed(MinMaxFalot Speed)
    {
        float negative = Speed.GetSpeed(_amoutRage);
        for (int i = 0; i < _ListNegativBost.Count; i++)
        {
            negative += (negative / 100) * _ListNegativBost[i];
        }
        negative -= Speed.GetSpeed(_amoutRage);

        float positive = Speed.GetSpeed(_amoutRage);
        for (int i = 0; i < _ListPositivBost.Count; i++)
        {
            positive += (positive / 100) * _ListPositivBost[i];
        }
        positive -= Speed.GetSpeed(_amoutRage);

        return Speed.GetSpeed(_amoutRage)+(negative+positive);
    }
}
