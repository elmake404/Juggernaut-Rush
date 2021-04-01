using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple door rotating script.
public class Door : MonoBehaviour {

    //Rotation speed.
    public float Speed = 1f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0f, 1f, 0f) * Time.deltaTime * Speed);	
	}
}
