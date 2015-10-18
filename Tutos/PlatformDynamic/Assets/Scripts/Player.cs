using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D rb2d;
	private float speedRef = 15.0f;
	private Vector2 forceRight;
	private Vector2 forceLeft;
	private Vector2 forceUp;
	private Vector2 forceDown;
	private float maxSpeed = 2.0f;



	// Use this for initialization
	void Start () {
		InitVectors();
		rb2d = GetComponent<Rigidbody2D>();
	}

	private void InitVectors()
	{
		forceRight = new Vector2(speedRef,0);
		forceLeft = new Vector2(-speedRef,0);
		forceUp = new Vector2(0,speedRef);
		forceDown = new Vector2(0,-speedRef);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.anyKey)
		{
			if(Input.GetKey(KeyCode.RightArrow) )
			{
				Debug.Log("Move right");
				rb2d.AddForce(forceRight);
			}
			if(Input.GetKey(KeyCode.LeftArrow) )
			{
				Debug.Log("Move left");
				rb2d.AddForce(forceLeft);
			}
			if(Input.GetKey(KeyCode.UpArrow) )
			{
				Debug.Log("Move up");
				rb2d.AddForce(forceUp);
			}
			if(Input.GetKey(KeyCode.DownArrow) )
			{
				Debug.Log("Move down");
				rb2d.AddForce(forceDown);
			}
		}
		rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
	}
}
