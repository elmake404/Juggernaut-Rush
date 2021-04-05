using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private GameObject _brokenGlass;
    //[SerializeField]
    //private ParticleSystem _FbxShards;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag=="Player")
    //    {
    //        other.GetComponent<Graviti>().RevertGraviti();
    //    }
    //}
    public void Breaking()
    {
        _meshRenderer.enabled = false;
        _collider.enabled = false;
        _brokenGlass.SetActive(true);
    }
    [ContextMenu("GetElevent")]
    private void GetElevent()
    {
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
}
