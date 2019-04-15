using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewContoller : MonoBehaviour {

    public float _moveSpeed = 5;
    public float mouseSpeed = 60;
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(-v* _moveSpeed, mouse* mouseSpeed, h* _moveSpeed) *Time.deltaTime, Space.World);

        
    }
}
