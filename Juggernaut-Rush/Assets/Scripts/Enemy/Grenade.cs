using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private RaycastHit _hit;
    [SerializeField]
    private ParticleSystem _explosion;
    [SerializeField]
    private SphereCollider _sphereCollider;
    [SerializeField]
    private MeshRenderer _mesh;
    private BezierSpline _spline;

    private float _pointPos;
    [SerializeField]
    private float _speed, _explosionRadius;
    [SerializeField]
    [Range(0,1f)]
    private float _timeExplosion;
    private float _time;
    void Start()
    {
        _sphereCollider.enabled = false;
        _time = 1f - _timeExplosion;
    }

    void FixedUpdate()
    {
        if (_pointPos < 1)
        {
            transform.position = _spline.GetPoint(_pointPos);
            _pointPos += _speed;
        }
        else if(_mesh.enabled)
        {
            StartCoroutine(Explosion());
        }
    }
    private IEnumerator Explosion()
    {
        _sphereCollider.radius = _explosionRadius;
        _sphereCollider.enabled = true;
        _mesh.enabled = false;
        _explosion.Play();
        yield return new WaitForSeconds(_timeExplosion);
        _sphereCollider.enabled = false;
        yield return new WaitForSeconds(_time);

        Destroy(gameObject);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
    public void InitializationSpline(BezierSpline spline)
    {
        _spline = spline;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (other.tag =="Player")
    //    //{
    //    //    Destroy(other.transform.parent.gameObject);
    //    //}
    //}
}
