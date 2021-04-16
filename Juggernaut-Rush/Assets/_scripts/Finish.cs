using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Finish Instance;

    [SerializeField]
    private Transform _positionJamp;
    public Transform GetPosJamp() => _positionJamp;

    private void Awake()
    {
        Instance = this;
    }
}
