using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardSurface : MonoBehaviour
{
    [SerializeField]
    private float _stressLevel; public float StressLevel
    {
        get
        {
            return _stressLevel;
        }
    }

    [SerializeField]
    private float _percentagofRageRecovery; public float PercentagofRageRecovery
    { get { return _percentagofRageRecovery; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
