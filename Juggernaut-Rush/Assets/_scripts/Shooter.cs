using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private Transform _shotPos;
    [SerializeField]
    private Bullet _bullet;
    private Transform _target { get { return PlayerLife.Instance.transform; } }
    [SerializeField]
    private BulletCharacteristics _bulletCharacteristics;

    [SerializeField]
    private float _delayTimeBeforeShot,_rotationSpeed;
    [SerializeField]
    private bool _isFollowThePlayer;
    private void Awake()
    {
        GameStageEvent.StartLevel += StartShot;
    }
    private void FixedUpdate()
    {
        if (_isFollowThePlayer)
        {
            FollowThePlayer();
        }
    }
    private void FollowThePlayer()
    {
        Vector3 posTarget = _target.position;
        posTarget.y = transform.position.y;
        posTarget.z += 2;
        Quaternion rotation = Quaternion.LookRotation(posTarget - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation,rotation, _rotationSpeed);
    }
    private void StartShot()
    {
        GameStageEvent.StartLevel -= StartShot;
        StartCoroutine(StartShoting());
    }
    private IEnumerator StartShoting()
    {
        while (true)
        {
            Shot();
            yield return new WaitForSeconds(_delayTimeBeforeShot);
        }
    }
    private void Shot()
    {
        Bullet bullet = Instantiate(_bullet, _shotPos.position, _shotPos.rotation);
        bullet.Initialization(_bulletCharacteristics);
    }
}
