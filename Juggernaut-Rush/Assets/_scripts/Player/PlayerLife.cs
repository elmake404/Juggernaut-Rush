using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    //[SerializeField]
    private List<GameObject> _listFloor;
    private PlayerMove _playerMove;
    public bool IsLife { get; private set; }
    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        IsLife = true;
    }
    private void FixedUpdate()
    {
        if (_listFloor != null && _listFloor.Count <= 0 && IsLife)
        {
            IsLife = false;
            Debug.Log(1);
            _playerMove.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            if (_listFloor == null)
            {
                _listFloor = new List<GameObject>();
            }
            _listFloor.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            _listFloor.Remove(other.gameObject);
        }
    }
}
