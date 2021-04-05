using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerMain;
    [SerializeField]
    private ParticleSystem _particleShot;
    [SerializeField]
    private Rigidbody _rbMain, _rbPelvis;
    [SerializeField]
    private Bullet _bullet;
    private Camera _cam;
    private Vector3 _startMosePos, _currentMosePos, _direcrionVector, _startPosPlayer;
    [SerializeField]
    private Transform _shotPos, _arm;
    [SerializeField]
    private Collider _feetCollider;
    [SerializeField]
    private GameObject _enemyTarget;
    [SerializeField]
    private Collider[] _collidersMain;
    [SerializeField]
    private GameObject _ragdoll;

    [SerializeField]
    private float _forceJump, _speedShot, _speedMoveDown, _speedMoveUp;
    private float _constSpeedShot;
    private void Awake()
    {
        _constSpeedShot = _speedShot;
        PlayerMain = this;
        _cam = Camera.main;
        _startPosPlayer = transform.position;
    }
    private void Update()
    {
        if (CanvasManager.IsStartGeme)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startMosePos = _cam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                if (_startMosePos == Vector3.zero)
                {
                    _startMosePos = _cam.ScreenToViewportPoint(Input.mousePosition);
                }

                _currentMosePos = _cam.ScreenToViewportPoint(Input.mousePosition);
                float window = (_startMosePos.y - _currentMosePos.y) > 0 ? 0.015f : -0.015f;

                Vector3 StartPosMose = _startMosePos;
                StartPosMose.y -= window;
                if (Mathf.Abs((_currentMosePos.y - _startMosePos.y) * 8) > 1)
                {
                    float yStart = ((_currentMosePos.y - _startMosePos.y) > 0 ? 0.125f : -0.125f);
                    _startMosePos.y = _currentMosePos.y - yStart;
                }

                float Y = 0;

                if (Mathf.Abs(_startMosePos.y - _currentMosePos.y) >= 0.015f)
                {
                    if (((_currentMosePos.y - StartPosMose.y) * 8) <= 0)
                    {
                        Y = ((_currentMosePos.y - StartPosMose.y) * 8) * _speedMoveDown;
                    }
                    else
                    {
                        Y = ((_currentMosePos.y - StartPosMose.y) * 8) * _speedMoveUp;
                    }

                    _direcrionVector = new Vector3(0, Y, 0);
                }
                else
                {
                    _direcrionVector = _rbMain.velocity;
                    _direcrionVector.y = 0;
                }

            }
            else
            {
                _direcrionVector = _rbMain.velocity;
                _direcrionVector.y = 0;
            }

        }
    }
    private void FixedUpdate()
    {
        if (CanvasManager.IsStartGeme)
        {
            ControlPosition();
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            _direcrionVector.x = _rbMain.velocity.x;
            _rbMain.velocity = _direcrionVector;
            RaycastHit hit;

            if (Mathf.Round(_rbMain.velocity.y) == 0 &&
                Physics.Raycast(_shotPos.position, _shotPos.forward, out hit) && hit.collider.tag != "Wall")
            {
                if (_speedShot <= 0)
                {
                    _particleShot.Play();
                    Instantiate(_bullet, _shotPos.position, _shotPos.rotation);
                    _speedShot = _constSpeedShot;
                }
                else
                {
                    _speedShot -= Time.fixedDeltaTime;
                }
            }
            else
            {
                _particleShot.Stop();
            }
            if (_enemyTarget != null)
            {
                Vector3 PosEnemy = _enemyTarget.transform.position;
                PosEnemy.y = transform.position.y;
                _arm.LookAt(PosEnemy);
            }
        }
        else
        {
            _particleShot.Stop();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            _rbMain.velocity = (transform.up * _forceJump);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Thorns" && !CanvasManager.IsWinGame)
        {
            CanvasManager.IsLoseGame = true;
            Death();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 12 && (_enemyTarget == null
            || (_enemyTarget.transform.position - transform.position).sqrMagnitude > (other.transform.position - transform.position).sqrMagnitude))
        {
            _enemyTarget = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            _enemyTarget = null;
        }
    }
    private void ControlPosition()
    {
        if (transform.position.y > _startPosPlayer.y)
        {
            Vector3 Pos = transform.position;
            Pos.y = _startPosPlayer.y;
            transform.position = Vector3.MoveTowards(transform.position, Pos, 0.7f);
        }
    }
    private void Death()
    {
        for (int i = 0; i < _collidersMain.Length; i++)
        {
            _collidersMain[i].enabled = false;
        }
        _ragdoll.SetActive(true);
        _rbMain.useGravity = true;
        gameObject.AddComponent<FixedJoint>().connectedBody = _rbPelvis;
        _rbMain.constraints = RigidbodyConstraints.None;
        enabled = false;

    }
    public Collider GetFeet()
    {
        return _feetCollider;
    }
}
