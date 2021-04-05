using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speedMove;
    [SerializeField]
    private ParticleSystem _break;
    void Start()
    {
        Destroy(gameObject, 2);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speedMove);
        RaycastHit hit;
        Vector3 Pos = transform.position + (transform.forward * (-10));
        if (Physics.Raycast(Pos, transform.forward,out hit,10))
        {
            if (hit.collider.tag == "Wall")
            {
                BulletHit(hit.point);
            }
            else if (hit.collider.tag == "Glass")
            {
                hit.collider.GetComponent<Glass>().Breaking();
            }
            else if (hit.collider.tag == "Head")
            {
                hit.collider.GetComponentInParent<EnemyLife>().Headshot();
                BulletHit(hit.point);
            }
            else if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<EnemyLife>().BodyShot();
                BulletHit(hit.point);
            }
        }
    }
    private void BulletHit(Vector3 posShot)
    {
        transform.position = posShot;
        _break.Play();
        _break.transform.SetParent(null);
        Destroy(_break.gameObject,1);
        Destroy(gameObject);
    }
}
