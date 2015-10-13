using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		if(body == null)
			Debug.Log("RigidBody2D null for Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.anyKey) {
			if(Input.GetKey(KeyCode.RightArrow))
			{
				Debug.Log("Move right");
				Vector2 startPosition = body.position;
				Vector2 endPosition = new Vector2(startPosition.x + 0.1f, startPosition.y);
				body.MovePosition(endPosition);
			}
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				Debug.Log("Move right");
				Vector2 startPosition = body.position;
				Vector2 endPosition = new Vector2(startPosition.x - 0.1f, startPosition.y);
				body.MovePosition(endPosition);
			}
			if(Input.GetKey(KeyCode.UpArrow))
			{
				Debug.Log("Move right");
				Vector2 startPosition = body.position;
				Vector2 endPosition = new Vector2(startPosition.x, startPosition.y + 0.1f);
				body.MovePosition(endPosition);
			}
			if(Input.GetKey(KeyCode.DownArrow))
			{
				Debug.Log("Move right");
				Vector2 startPosition = body.position;
				Vector2 endPosition = new Vector2(startPosition.x, startPosition.y - 0.1f);
				body.MovePosition(endPosition);
			}
		} else {
			body.velocity = Vector3.zero;
		}

	}
}
