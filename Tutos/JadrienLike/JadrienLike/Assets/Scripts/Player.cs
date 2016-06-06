using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    #region private Unity objects
    private Animator animator;
    private Rigidbody2D rb2d;
    #endregion

    public float speed = 50f;
    public float jumpPower = 150f;
    public bool grounded = true;
    public Text textCount;
    private int count = 0;
    public Vector2 knockback;
    private bool untouchable = false;
    public int FootHit = 20;
    public int weaponDamage = 40;
    public Text textMental;
    private bool backward = false;
    private int _mental = 50;

    #region Accessors
    public int Mental
    {
        get
        {
            return _mental;
        }
    }
    #endregion

    void Start ()
    {
        animator = GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        setCoins(count);
        setMental(_mental);
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
            if (backward)
            {
                Flip();
            }
        }
        else if (h < 0)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            animator.SetBool("backward", true);
            if (!backward)
            {
                Flip();
            }
        }
        else
        {
            animator.SetTrigger("Rest");
        }
        if (Input.GetButton("Jump") && grounded) rb2d.AddForce(Vector2.up * jumpPower);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCoins(count);
        }
        else if (other.gameObject.CompareTag("MentalUp"))
        {
            other.gameObject.SetActive(false);
            _mental += Constantes.mentalUp;
            setMental(_mental);
        }
        else if (other.gameObject.CompareTag("MentalDown"))
        {
            other.gameObject.SetActive(false);
            _mental += Constantes.mentalDown;
            setMental(_mental);
        }
        else if (other.gameObject.CompareTag("Enemy") && !untouchable)
        {
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
            Damage(enemy.Damage, enemy.transform.position);
        }
    }
    public void Flip()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
        backward = !backward;
    }
    // If the player touches the enemy
    // TODO: maybe remove
    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.CompareTag("Enemy") && !untouchable)
        {
            Enemy enemy = col.gameObject.GetComponentInParent<Enemy>();
            enemy.OnHit();
            Damage(enemy.Damage, enemy.transform.position);
        }

    }

    void setCoins(int count)
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
        if (!untouchable)
        {
            becomesUntouchable();
            _mental -= hit;
            setMental(_mental);
            gameObject.GetComponent<Animation>().Play("RedFlash");
            Vector2 direction = new Vector2(attacker.x - transform.position.x, attacker.y - transform.position.y);
            direction.Normalize();
            rb2d.AddForce(new Vector2(direction.x * -knockback.x, direction.y * knockback.y));
        }
    }

    private void becomesUntouchable()
    {
        // Time in ms
        int untouchTime = 2000;
        untouchable = true;
        Timer t = new Timer(new TimerCallback(stopsBeingUntouchableCallback));
        t.Change(untouchTime, 0);
    }   

    private void stopsBeingUntouchableCallback(object state)
    {
        untouchable = false;
        Timer t = (Timer) state;
        if (t != null)
            t.Dispose();
    }

    public void Attack(Enemy enemy, int hit)
    {
        enemy.OnHit();
        enemy.OnHurt(hit);
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
