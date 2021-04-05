using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Transform _player;
    private Vector3 _offSet;
    void Start()
    {
        
        _player = Player.PlayerMain.transform;
        transform.Translate(Vector3.up * 10);
        _offSet = _player.position - transform.position;
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = (_player.position - _offSet).y;
        transform.position = pos;
    }
}
