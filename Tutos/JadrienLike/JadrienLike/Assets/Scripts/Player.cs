using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    #region Unity debug only
    public bool easyKill = true;
    #endregion

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
    private int _health = 100;

    private bool _isOnLadder = false;

    public Vector3 initPosition;

    private Enemy _onTop = null;
    #region Accessors
    public int Mental
    {
        get
        {
            return _mental;
        }
        set
        {
            if(value >= 0 && value <= 100)
            {
                _mental = value;
            } 
            else if (value < 0)
            {
                _mental = 0;
            }
            else if (value > 100)
            {
                _mental = 100;
            }
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if(value >= 0 && value <= 100)
            {
                _health = value;
            } 
            else if (value < 0)
            {
                _health = 0;
            }
            else if (value > 100)
            {
                _health = 100;
            }
        }
    }

    public Enemy OnTop
    {
        get
        {
            return _onTop;
        }
        set
        {
            _onTop = value;
        }
    }
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //setCoins(count);
        //setMental(_mental);
    }

    void Update()
    {
        animator.SetBool("grounded", grounded);
    }
    #region Ladder
    public void OnEnterLadder(Ladder ladder)
    {
        _isOnLadder = true;
        animator.SetBool("On_Ladder", _isOnLadder);
        rb2d.isKinematic = true;

        float ladderMiddleX = ladder.transform.position.x;
        transform.position = new Vector3(ladderMiddleX, transform.position.y, transform.position.z);

        grounded = true;

        Hat _hat = this.GetComponentInChildren<Hat>();
        _hat.GetComponent<Renderer>().enabled = false;
    }

    public void OnStayLadder()
    {
        _isOnLadder = true;
        animator.SetBool("On_Ladder", _isOnLadder);
        grounded = true;
    }

    public void OnExitLadder()
    {
        _isOnLadder = false;
        rb2d.isKinematic = false;
        grounded = false;

        animator.SetBool("On_Ladder", _isOnLadder);
        Hat _hat = this.GetComponentInChildren<Hat>();
        _hat.GetComponent<Renderer>().enabled = backward ? false : true;

    }

    public void MoveUp()
    {
        animator.SetBool("Ladder_Climb", true);
        //rb2d.AddForce((Vector2.up * speed));
        float currentY = transform.position.y;
        transform.position = new Vector3(transform.position.x, currentY + 0.01f, transform.position.z);
    }

    public void MoveDown()
    {
        animator.SetBool("Ladder_Climb", true);
        //rb2d.AddForce((Vector2.down * speed));
        float currentY = transform.position.y;
        transform.position = new Vector3(transform.position.x, currentY - 0.05f, transform.position.z);
    }
    #endregion Ladder

    public void Teleport(Vector3 destination)
    {
        //transform.position = destination;
        transform.position = initPosition;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (!_isOnLadder || !grounded)
        {
            if (h >= 1 || h <= -1)
            {
                rb2d.AddForce((Vector2.right * speed) * h);
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }


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
                animator.SetTrigger("Walk");
                if (!backward)
                {
                    Flip();
                }
            }
            else
            {
                animator.SetTrigger("Rest");
            }
        }
        if (Input.GetButton("Jump") && grounded)
        {
            animator.SetBool("On_Ladder", false);
            animator.SetTrigger("Jump");
            // We can jump while being on a Ladder, thus we need to get back to non kinematic
            bool isOnLadder = rb2d.isKinematic;
            if (isOnLadder)
            {
                rb2d.isKinematic = false;
            }
            rb2d.AddForce(Vector2.up * jumpPower);

        }
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
            Mental = Mental + Constantes.mentalUp;
        }
        else if (other.gameObject.CompareTag("MentalDown"))
        {
            other.gameObject.SetActive(false);
            Mental = Mental + Constantes.mentalDown;
        }
        else if (other.gameObject.CompareTag("Enemy") && !untouchable)
        {
            Debug.Log("ontop " + this.OnTop + " other " + other.GetComponent<Enemy>());
            if (other.enabled &&  !this.OnTop.Equals(other.GetComponent<Enemy>()))
            {
                Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
                Damage(enemy.Damage, enemy.transform.position);
            }
        }
    }
    public void Flip()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
        backward = !backward;
        Hat _hat = this.GetComponentInChildren<Hat>();
        _hat.GetComponent<Renderer>().enabled = backward ?  false :  true;
    }
    // If the player touches the enemy
    // TODO: maybe remove
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && !untouchable)
        {
            Enemy enemy = col.gameObject.GetComponentInParent<Enemy>();
            enemy.OnHit();
            Damage(enemy.Damage, enemy.transform.position);
        }
    }
    [Obsolete("Will be removed soon")]
    void setCoins(int count)
    {
        textCount.text = "Coins : " + count;
    }

    public void Damage(int hit, Vector3 attacker)
    {
        if (easyKill)
        {
            hit = 1000;
        }
        if (!untouchable || easyKill)
        {
            becomesUntouchable();
            Mental = Mental - hit;
            //setMental(_mental);
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
        Timer t = (Timer)state;
        if (t != null)
            t.Dispose();
    }

    public void Attack(Enemy enemy, int hit)
    {
        if (enemy.isActiveAndEnabled)
        {
            enemy.OnHit();
            enemy.OnHurt(hit);
        }
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
