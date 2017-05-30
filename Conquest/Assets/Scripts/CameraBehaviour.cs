using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 initialOffset = new Vector3(5.0f, 5.0f, 0.0f);
        MoveCamera(initialOffset);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveCamera(new Vector3(0.0f, 1.0f, 0.0f));
        if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveCamera(new Vector3(0.0f, -1.0f, 0.0f));
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveCamera(new Vector3(-1.0f, 0.0f, 0.0f));
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveCamera(new Vector3(1.0f, 0.0f, 0.0f));
    }

    private void MoveCamera(Vector3 move)
    {
        transform.position += move;
    }
}
