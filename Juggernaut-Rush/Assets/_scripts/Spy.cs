using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : MonoBehaviour
{
    [SerializeField]
    private Transform _surveillanceObj;

    void LateUpdate()
    {
        transform.SetPositionAndRotation(_surveillanceObj.position,_surveillanceObj.rotation);
    }
}
