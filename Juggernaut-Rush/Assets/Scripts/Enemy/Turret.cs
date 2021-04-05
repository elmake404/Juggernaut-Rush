using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _fireFbx;
    [SerializeField]
    private Collider _colliderFire;

    [SerializeField]
    private float _timeFire=1, _timeFirePase=1;
    void Start()
    {
        StartCoroutine(FireByFire());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FireByFire()
    {
        while (true)
        {
            _fireFbx.Play();
            _colliderFire.enabled = true;
            yield return new WaitForSeconds(_timeFire);
            _fireFbx.Stop();
            _colliderFire.enabled = false;
            yield return new WaitForSeconds(_timeFirePase);
        }
    }
}
