using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeObj: MonoBehaviour
{
    [SerializeField]
    protected GameObject _wallWreckage;
    [SerializeField]
    private float _percentagofRageRecovery;public float PercentagofRageRecovery 
    { get { return _percentagofRageRecovery; } }
    [SerializeField]
    private float _stressLevel; public float StressLevel
    {
        get
        {
            return _stressLevel;
        }
    }

    public virtual void ActivationWallWreckage()
    {
        _wallWreckage.SetActive(true);
        Destroy(gameObject);
    }
}
