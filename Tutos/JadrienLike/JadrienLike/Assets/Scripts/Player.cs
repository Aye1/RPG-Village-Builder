using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

   private Animator animator;
    public float speed = 50f;
    public float jumpPower = 150f;
    public bool grounded = true;

    private Rigidbody2D rb2d;

    void Start ()
    {
        animator = GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      animator.SetBool("grounded", grounded);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float gravity = rb2d.velocity.y;
        if (h !=0)
         rb2d.AddForce((Vector2.right * speed) * h);
        else rb2d.velocity = new Vector2(0, rb2d.velocity.y);

        if (h > 0)
        {
            rb2d.velocity = new Vector2(speed, gravity);
            animator.SetTrigger("Walk");
            animator.SetBool("backward", false);
        }
        else if (h < 0)
        {
            rb2d.velocity = new Vector2(-speed,gravity);
            animator.SetTrigger("Walk Backward");
            animator.SetBool("backward", true);
        }
        else
        {
            if (animator.GetBool("backward")) animator.SetTrigger("Rest Backward");
            else  animator.SetTrigger("Rest");
        }


        if (Input.GetButton("Jump") && grounded) rb2d.AddForce(Vector2.up * jumpPower);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            other.gameObject.SetActive(false);
        }
    }


}
