using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public delegate void ActionFloat(float item);
    public event ActionFloat RageAmount;

    private List<GameObject> _listFloor = new List<GameObject>();
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rbMain;
    [SerializeField]
    private Material _meshMaterial;
    [SerializeField]
    private Color _colorRage = Color.red, _colorOfCalm = Color.white;
    private Color _differenceColor;

    [SerializeField]
    private float _timeRage;
    private float _timerRage;

    private void Awake()
    {
        var gameStage = FindObjectOfType<GameStage>();
        gameStage.StartLevel += StartAnimationRun;
    }
    private void Start()
    {
        _timerRage = _timeRage;
        _meshMaterial.color = _colorRage;
        _differenceColor = _colorOfCalm - _colorRage;
    }
    private void FixedUpdate()
    {
        if (GameStage.IsGameFlowe)
        {
            if (_timerRage > 0)
            {
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            _listFloor.Add(other.gameObject);
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
    public void GameOver()
    {
        _rbMain.constraints = RigidbodyConstraints.FreezeRotation;
        _rbMain.AddForce(Vector3.forward * 500, ForceMode.Acceleration);
        transform.GetChild(0).gameObject.layer = 8;

        GameStage.Instance.ChangeStage(Stage.LostGame);
    }
    private void StartAnimationRun()
    {
        _animator.SetBool("Run", true);
    }

    public float GetAmoutRage() => (_timerRage / _timeRage);
    public void RestoringRage(float procent)
    {
        _timerRage += _timeRage / 100 * procent;
        if (_timerRage>_timeRage)
        {
            _timerRage = _timeRage;
        }
    }
}
