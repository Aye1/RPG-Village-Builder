using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ManageInput();
	}

    private void ManageInput()
    {
        Vector3 move = Vector3.zero;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            move = Vector3.left + Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move = Vector3.right + Vector3.back;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            move = Vector3.forward + Vector3.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move = Vector3.back + Vector3.left;
        }
        transform.position += move;
    }
}
