using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private List<GameObject> _listFloor = new List<GameObject>();
    private PlayerMove _playerMove;
    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
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
                _playerMove.GameOver();
            }
        }
    }
}
