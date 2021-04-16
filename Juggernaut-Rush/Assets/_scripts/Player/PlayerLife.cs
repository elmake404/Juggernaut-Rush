using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife Instance;
    public static bool IsGetAngry;

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
    private float _timerRage;

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
            _timerRage = _timeRage;
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
                if (_timerRage > 0)
                {
                    _animator.speed = _playerMove.GetAmoutSpeed();
                    _timerRage -= Time.deltaTime;
                }
                else
                {
                    _animator.SetBool("Death", true);
                    _animator.SetBool("Run", false);
                    GameStage.Instance.ChangeStage(Stage.LostGame);
                }

            _meshMaterial.color = _colorRage + (_differenceColor / 100) * ((_timeRage - _timerRage) / _timeRage * 100);
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
           StartCoroutine( WinGame(Finish.Instance.GetPosJamp()));
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
                _animator.SetBool("Fly", true);
                _animator.SetBool("Run", false);
                GameOver();
            }
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
            _animator.SetBool("GetAngry", true);
            StartCoroutine(Angry());
        }

        GameStageEvent.StartLevel -= StartAnimationRun;
    }
    private void GameOver()
    {
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

        _rbMain.velocity =jampDirection.forward * (v /*- (v / 10)*/);
    }
    private IEnumerator Angry()
    {
        float timeToRage = 1.5f;
        while (timeToRage > 0)
        {
            timeToRage -= Time.fixedDeltaTime;
            _timerRage = Mathf.Lerp(_timerRage, _timeRage, 0.3f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        IsGetAngry = true;
        _steem.Play();

        _animator.SetBool("Run", true);

    }
    public float GetAmoutRage() => (_timerRage / _timeRage);
    public void RestoringRage(float procent)
    {
        _timerRage += _timeRage / 100 * procent;
        if (_timerRage > _timeRage)
        {
            _timerRage = _timeRage;
        }
    }
}
