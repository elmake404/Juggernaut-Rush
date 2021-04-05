using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rbMain;
    void Start()
    {
        _rbMain.AddForce(-transform.right*200);
        Destroy(gameObject,2);
    }

    [ContextMenu("GetRigidbody")]
    private void GetRigidbody()
    {
        _rbMain = GetComponent<Rigidbody>();
    }
}
