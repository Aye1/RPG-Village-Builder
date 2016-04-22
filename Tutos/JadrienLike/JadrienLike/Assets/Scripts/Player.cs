using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

   private Animator animator;
    private float movex;
    private float movey;
    public float speed;
    public Vector2 acceleration;
    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        movex = Input.GetAxis("Horizontal");
        //movey = Input.GetAxis("Vertical"); //jetpack
        movey = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(movex * speed, movey * speed);

        if (movex > 0)
        {
            animator.SetTrigger("Walk");
            animator.SetBool("backward", false);
        }
        else if (movex < 0)
        {
            animator.SetTrigger("Walk Backward");
            animator.SetBool("backward", true);
        }
        else
        {
            if (animator.GetBool("backward")) animator.SetTrigger("Rest Backward");
            else  animator.SetTrigger("Rest");
        }
       

        if (Input.GetButton("Jump") )
        {
            GetComponent<Rigidbody2D>().AddForce(acceleration, ForceMode2D.Impulse);
        }
        }
}
