using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour {

   private Animator animator;
    public float speed = 50f;
    public float jumpPower = 150f;
    public bool grounded = true;
    public Text textCount;
    private int count = 0;
    public Vector2 knockback;

    public Text textMental;
    private int mental = 50;


    private Rigidbody2D rb2d;

    void Start ()
    {
        animator = GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        setCount(count);
        setMental(mental);
    }

    void Update()
    {
      animator.SetBool("grounded", grounded);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h >= 1 || h <= -1)
         rb2d.AddForce((Vector2.right * speed) * h);
        else    rb2d.velocity = new Vector2(0, rb2d.velocity.y);

        if (h > 0)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            animator.SetTrigger("Walk");
            animator.SetBool("backward", false);
        }
        else if (h < 0)
        {
            rb2d.velocity = new Vector2(-speed,rb2d.velocity.y);
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
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCount(count);
        }
     else   if (other.gameObject.CompareTag("MentalUp"))
        {
            other.gameObject.SetActive(false);
            MentalUp addMental = other.GetComponent<MentalUp>();
            mental += addMental.Value;
            setMental(mental);
        }
        else if (other.gameObject.CompareTag("MentalDown"))
        {
            other.gameObject.SetActive(false);
            MentalDown addMental = other.GetComponent<MentalDown>();
            mental += addMental.Value;
            setMental(mental);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.CompareTag("Enemy"))
        {
          
            Damage(col.gameObject.GetComponentInParent<Clock>().damage, col.gameObject.transform.position);
        }

    }

    void setCount(int count)
    {
        textCount.text = "Coins : "+count;
    }

    void setMental(int mental)
    {
       
        if (mental >= 100)
        {
            textMental.text = "Passage en mode Lucide";
        }
        else if (mental <=0)
        {
            textMental.text = "Passage en mode Cauchemar";
        }
        else
        {
            textMental.text = "Mental : " + mental + "/100";
        }
    }

    public void Damage(int hit, Vector3 attacker)
    {
        mental -= hit;
        setMental(mental);
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");
        Vector2 direction = new Vector2(attacker.x - transform.position.x, attacker.y - transform.position.y);
        direction.Normalize();
        rb2d.AddForce(new Vector2 (direction.x * -knockback.x, direction.y * knockback.y));

    }
    /* public IEnumerator Knockback(float knockbackDur, float knockbackpwr, Vector3 knockbackDir)
     {
         float timer = 0;
         while (knockbackDur > timer)
         {
             timer += Time.deltaTime;
             Debug.Log("damage " + rb2d.velocity);
             // rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackpwr, transform.position.z));
             rb2d.velocity += new Vector2(knockbackDir.x * -10, knockbackDir.y * knockbackpwr);
             Debug.Log("damage "+rb2d.velocity);
         }

         yield return 0;

            //to launch
            // StartCoroutine(Knockback(0.01f, 10, transform.position));
     }*/
}
