using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife Instance;
    public static bool IsGetAngry, IsInFlight;

    private List<GameObject> _listFloor = new List<GameObject>();
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rbMain;
    [SerializeField]
    private Material _meshMaterial;
    private PlayerMove _playerMove;
    [SerializeField]
    private ParticleSystem _steem;

    [SerializeField]
    private Color _colorRage = Color.red, _colorOfCalm = Color.white;
    private Color _differenceColor;

    [SerializeField]
    private float _timeRage;
    [SerializeField]
    [Range(0, 1)]
    private float _powerOfUnstoppability, _startPrecentRage;
    private float _timerRage;
    public float PowerOfUnstoppability
    { get { return _powerOfUnstoppability; } }
    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        Instance = this;
        GameStageEvent.StartLevel += StartAnimationRun;
    }
    private void OnApplicationQuit()
    {
        GameStageEvent.StartLevel -= StartAnimationRun;
    }
    private void Start()
    {
        if (IsGetAngry)
        {
            _timerRage = _timeRage * _startPrecentRage;
            _meshMaterial.color = _colorRage;
        }
        else
        {
            _meshMaterial.color = _colorOfCalm;
        }

        _differenceColor = _colorOfCalm - _colorRage;
    }
    private void FixedUpdate()
    {
        if (GameStage.IsGameFlowe)
        {
            if (IsGetAngry)
                if (_timerRage > 0.01f)
                {
                    _animator.speed = _playerMove.GetAmoutSpeed();
                    _timerRage -= Time.deltaTime;
                }
            //else
            //{
            //    Death();
            //}

            //_meshMaterial.color = _colorRage + (_differenceColor / 100) * ((_timeRage - _timerRage) / _timeRage * 100);

            _meshMaterial.color = Vector4.Lerp(_meshMaterial.color, GetColor(), 0.3f);
        }
        else
        {
            _animator.speed = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            _listFloor.Add(other.gameObject);
        }

        if (other.gameObject == Finish.Instance.gameObject)
        {
            _animator.SetBool("Win", true);
            _animator.SetBool("Run", false);
            GameStage.Instance.ChangeStage(Stage.WinGame);
            StartCoroutine(WinGame(Finish.Instance.GetPosJamp()));
            IsGetAngry = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            _listFloor.Remove(other.gameObject);
            if (_listFloor.Count <= 0)
            {
                FellFromAHeight();
            }
        }
    }
    private Color GetColor()
    {
        if (GetAmoutRage() > _powerOfUnstoppability)
        {
            return _colorRage;
        }
        else
        {
            return _colorOfCalm;
        }
    }
    private void StartAnimationRun()
    {
        if (IsGetAngry)
        {
            _steem.Play();
            _animator.SetBool("Run", true);
        }
        else
        {
            if (!IsInFlight)
            {
                _animator.SetInteger("GetAngry", 1);
                StartCoroutine(Angry(1.5f));
                IsInFlight = true;
            }
            else
            {
                _animator.SetInteger("GetAngry", 2);
                StartCoroutine(Angry(2f));
            }
        }

        GameStageEvent.StartLevel -= StartAnimationRun;
    }
    private void FellFromAHeight()
    {
        _animator.SetBool("Fly", true);
        _animator.SetBool("Run", false);

        _steem.Stop();

        _rbMain.constraints = RigidbodyConstraints.FreezeRotation;
        _rbMain.AddForce(Vector3.forward * 500, ForceMode.Acceleration);
        transform.GetChild(0).gameObject.layer = 8;

        GameStage.Instance.ChangeStage(Stage.LostGame);
    }
    private IEnumerator WinGame(Transform jampDirection)
    {
        _steem.Stop();

        yield return new WaitForSeconds(0.4f);
        Vector3 PosJamp = jampDirection.position;
        PosJamp.x = transform.position.x;
        _rbMain.constraints = RigidbodyConstraints.FreezeRotation;
        Vector3 fromTo = PosJamp - transform.position;
        Vector3 from = new Vector3(fromTo.x, 0, fromTo.z);
        jampDirection.rotation = Quaternion.LookRotation(from, Vector3.up);
        jampDirection.Rotate(Vector3.right, -35);

        float x = from.magnitude;
        float y = fromTo.y;
        float Angel = -35 * Mathf.PI / 180;

        float v2 = ((20f * x * x) / (2 * (y - Mathf.Tan(Angel) * x) * Mathf.Pow(Mathf.Cos(Angel), 2)));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        _rbMain.velocity = jampDirection.forward * (v /*- (v / 10)*/);
    }
    private IEnumerator Angry(float time)
    {
        float timeToRage = time;
        while (timeToRage > 0)
        {
            timeToRage -= Time.fixedDeltaTime;
            _timerRage = Mathf.Lerp(_timerRage, _timeRage * _startPrecentRage, 0.3f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        IsGetAngry = true;
        _steem.Play();

        _animator.SetBool("Run", true);

    }
    public void Death()
    {
        _steem.Stop();
        _animator.SetBool("Death", true);
        _animator.SetBool("Run", false);
        GameStage.Instance.ChangeStage(Stage.LostGame);
    }
    public float GetAmoutRage() => (_timerRage / _timeRage);
    public void RestoringRage(float procent)
    {
        _timerRage += _timeRage / 100 * procent;

        if (_timerRage > _timeRage)
        {
            _timerRage = _timeRage;
        }
        else if (0 > _timerRage && procent < 0)
        {
            _timerRage = 0;
            Death();
        }
    }
}
