using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	private Rigidbody2D body;
	private Animator animator;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		if(body == null)
			Debug.Log("RigidBody2D null for Player");
		animator = GetComponent<Animator>();
		if(animator == null)
			Debug.Log("Animator null for Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.anyKey) {
			ResetDirectionBool();
			if(Input.GetKey(KeyCode.RightArrow))
			{
				Debug.Log("Move right");
				animator.SetBool("Go_Right", true);
				Vector2 startPosition = body.position;
				Vector2 endPosition = new Vector2(startPosition.x + 0.1f, startPosition.y);
				body.MovePosition(endPosition);
			}
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				Debug.Log("Move left");
				animator.SetBool("Go_Left", true);
				Vector2 startPosition = body.position;
				Vector2 endPosition = new Vector2(startPosition.x - 0.1f, startPosition.y);
				body.MovePosition(endPosition);
			}
			if(Input.GetKey(KeyCode.UpArrow))
			{
				Debug.Log("Move up");
				animator.SetBool("Go_Up", true);
				Vector2 startPosition = body.position;
				Vector2 endPosition = new Vector2(startPosition.x, startPosition.y + 0.1f);
				body.MovePosition(endPosition);
			}
			if(Input.GetKey(KeyCode.DownArrow))
			{
				Debug.Log("Move down");
				animator.SetBool("Go_Down", true);
				Vector2 startPosition = body.position;
				Vector2 endPosition = new Vector2(startPosition.x, startPosition.y - 0.1f);
				body.MovePosition(endPosition);
			}
		} else {
			body.velocity = Vector3.zero;
		}

	}

	private void ResetDirectionBool() 
	{
		animator.SetBool("Go_Right", false);
		animator.SetBool("Go_Left", false);
		animator.SetBool("Go_Down", false);
		animator.SetBool("Go_Up", false);
	}
}
