using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Wreckage
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _ragdoll, _shotPos;
    [SerializeField]
    private Bullet _bullet;
    private Transform _target
    { get { return PlayerLife.Instance.transform; } }
    [SerializeField]
    private BulletCharacteristics _bulletCharacteristics;

    [SerializeField]
    private float _foreseMultiplier, _delayTimeBeforeShot, _rotationSpeed;
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
        Quaternion rotation = Quaternion.LookRotation(posTarget - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed);
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
    private IEnumerator ActivationAnimation(string parameter)
    {
        _animator.SetBool(parameter, true);
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool(parameter, false);
    }
    private void Shot()
    {
        if (_animator!=null)
        {
            StartCoroutine(ActivationAnimation("Shot"));
        }
        Bullet bullet = Instantiate(_bullet, _shotPos.position, _shotPos.rotation);
        bullet.Initialization(_bulletCharacteristics);
    }
    public override void PushWreckage(Vector3 direction, Vector3 contactPoint, float forse)
    {
        ////
        #region crutch
        direction.y /= 4;
        direction.z *= 2;
        #endregion

        _ragdoll.gameObject.SetActive(true);
        _ragdoll.parent.SetParent(null);
        _rbWreckage.AddForce(direction * forse*_foreseMultiplier, ForceMode.Acceleration);
        Destroy(gameObject);
    }
    public override void Explosion(float forese, Vector3 positionExplosion, float radius)
    {
        _ragdoll.gameObject.SetActive(true);
        _ragdoll.parent.SetParent(null);

        _rbWreckage.AddExplosionForce(forese * _foreseMultiplier/2, positionExplosion, radius, 0, ForceMode.Acceleration);
        Destroy(gameObject);
    }
}
