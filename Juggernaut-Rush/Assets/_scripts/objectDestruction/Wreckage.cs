using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wreckage : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody _rbWreckage;
    [SerializeField]
    private FixedJoint _fixedJoint;

    [SerializeField]
    private bool _hardSurface;
    [SerializeField]
    private float _stressLevel; public float StressLevel
    {
        get
        {
            return _stressLevel;
        }
    }

    [SerializeField]
    private float _percentagofRageRecovery; public float PercentagofRageRecovery
    { get { return _percentagofRageRecovery; } }
    [ContextMenu("Initialization")]
    private void Initialization()
    {
        _rbWreckage = GetComponent<Rigidbody>();
        _fixedJoint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
    public virtual void PushWreckage(Vector3 direction,Vector3 contactPoint,float forse)
    {
        ////
        #region crutch
        direction.y /= 4;
        direction.z *= 2;
        #endregion

        Destroy(_fixedJoint);
        transform.SetParent(null);

        _rbWreckage.AddForceAtPosition(direction*forse, contactPoint, ForceMode.Acceleration);

        enabled = false;
        Destroy(gameObject,2);
    }
    public virtual void Explosion(float forese,Vector3 positionExplosion,float radius)
    {
        Destroy(_fixedJoint);
        transform.SetParent(null);

        _rbWreckage.AddExplosionForce(forese, positionExplosion, radius,0,ForceMode.Acceleration);

        enabled = false;
        Destroy(gameObject, 2);
    }
    public bool TestOfStrength(float RageAmout)
    {
        if (_hardSurface)
        {
            if (RageAmout >= PlayerLife.Instance.PowerOfUnstoppability)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}
