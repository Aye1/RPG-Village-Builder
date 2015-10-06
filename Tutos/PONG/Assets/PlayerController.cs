using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10;

    public string MoveUp;
    public string MoveDown;

	// Update is called once per frame
	void Update () {
	if( Input.GetKey(MoveUp))
        {
//            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(MoveDown))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
