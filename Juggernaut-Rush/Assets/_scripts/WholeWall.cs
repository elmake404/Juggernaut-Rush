using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeWall : MonoBehaviour
{
    [SerializeField]
    private GameObject _wallWreckage;
    public void ActivationWallWreckage()
    {
        _wallWreckage.SetActive(true);
        Destroy(gameObject);
    }
}
