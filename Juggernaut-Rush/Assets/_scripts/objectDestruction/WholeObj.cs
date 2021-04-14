using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeObj: MonoBehaviour
{
    [SerializeField]
    protected GameObject _wallWreckage;

    public virtual void ActivationWallWreckage()
    {
        _wallWreckage.SetActive(true);
        Destroy(gameObject);
    }
}
