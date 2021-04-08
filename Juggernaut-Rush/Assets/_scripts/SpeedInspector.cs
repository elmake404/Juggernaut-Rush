using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedInspector : MonoBehaviour
{
    [SerializeField]
    private float _bonus, _timeOfAction;
    
    public float Bonus { get { return _bonus; } }
    public float TimeOfAction { get { return _timeOfAction; } }

}
