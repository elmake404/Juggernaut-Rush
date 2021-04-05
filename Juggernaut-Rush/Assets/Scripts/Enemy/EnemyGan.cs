using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGan : MonoBehaviour
{
    [SerializeField]
    private EnemyLife _lifeMain;
    [SerializeField]
    private Transform _shotPos, _shotgunMod, _shotgun;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private BulletEnemy _bullet;
    private Player _player;
    [SerializeField]
    private float _speedRot, _delayShot;
    [SerializeField]
    private float _upActivation, _downActivtion;

    void Start()
    {
        _player = Player.PlayerMain;
        StartCoroutine(StartShooting());
    }
    void FixedUpdate()
    {
        _shotgun.transform.SetPositionAndRotation
            (_shotgunMod.transform.position, _shotgunMod.transform.rotation);
        if (!_lifeMain.Life)
        {
            Death();
        }
    }
    private IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            float PosY = (_player.transform.position.y - transform.position.y);

            if (PosY <= _upActivation && PosY >= _downActivtion)
            {
                _animator.SetBool("Shot", true);
                yield return new WaitForSeconds(1f);
                _animator.SetBool("Shot", false);

                Quaternion RotBullet = Quaternion.Euler(_shotPos.eulerAngles.x, _shotPos.eulerAngles.y, 0);
                Instantiate(_bullet, _shotPos.position, RotBullet);
                yield return new WaitForSeconds(1.533f);
                yield return new WaitForSeconds(_delayShot);
            }
            else
            {
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    private void Death()
    {
        _animator.enabled = false;
        _shotgun.gameObject.SetActive(false);
        //Debug.Log(_animator.enabled);
        Destroy(gameObject);
    }
}
