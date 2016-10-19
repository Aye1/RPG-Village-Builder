using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	private Animator animator;

	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator>();

		base.Start();
	}

	private void CheckIfGameOver()
	{
		//TODO
	}

	protected override void AttemptMove (int xDir, int yDir)
	{
		base.AttemptMove (xDir, yDir);
		RaycastHit2D hit;
	}

	// Update is called once per frame
	void Update () {
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int) Input.GetAxisRaw ("Horizontal");
		vertical = (int) Input.GetAxisRaw ("Vertical");

		if (horizontal != 0 || vertical != 0)
		{
			AttemptMove (horizontal, vertical);	
		}
	}

	protected override void OnCantMove<T> (T component) 
	{
	}

	private void Restart()
	{

	}
}
