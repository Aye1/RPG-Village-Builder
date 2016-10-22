using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{

    #region Unity debug only
    [Header("Unity debug")]
    public bool easyKill = true;
    #endregion

    #region private Unity objects
    private Animator animator;
    private Rigidbody2D rb2d;
    public AudioSource audioSource;
    public AudioClip[] jumpClips;
    #endregion

    [Header("Move Characteristics")]
    public float speed = 50f;
    public float maxVelocity = 20.0f;
    public float jumpPower = 800f;
    public bool grounded = true;
    public Vector3 initPosition;
    public bool backward = false;

    [Header("Attack Characteristics")]
    public int FootHit = 20;
    public int weaponDamage = 50;
    public Vector2 knockback;

    public Text textCount;
    private int _spiritCount = 0;
    private bool untouchable = false;
    private int _mental = 50;
    private int _health = 100;
    private bool _isOnLadder = false;
    private int _countLadder = 0;
    private Enemy _onTop = null;
    private bool doorTaken;

    private int _maxSpirit = 1000;

    private static Player instance = null;

    #region Accessors
    public int Mental
    {
        get
        {
            return _mental;
        }
        set
        {
            if (value >= 0 && value <= 100)
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
            if (value >= 0 && value <= 100)
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

    public int Spirit
    {
        get
        {
            return _spiritCount;
        }
        set
        {
            if (value >= 0 && value <= _maxSpirit)
            {
                _spiritCount = value;
            }
            else if (value < 0)
            {
                _spiritCount = 0;
            }
            else if (value > _maxSpirit)
            {
                _spiritCount = _maxSpirit;
            }
        }
    }
    #endregion

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        animator = GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        animator.SetBool("grounded", grounded);
        LimitVelocity();
        GameObject.FindGameObjectWithTag("hat").GetComponent<Renderer>().enabled = backward || _isOnLadder ? false : true;
    }

    /// <summary>
    /// Limits the velocity of the player (useful for falls)
    /// </summary>
    private void LimitVelocity()
    {
        if (rb2d.velocity.SqrMagnitude() > maxVelocity * maxVelocity)
        {
            rb2d.velocity = maxVelocity * rb2d.velocity.normalized;
        }
    }

    #region Ladder
    public void OnEnterLadder(Ladder ladder)
    {
        _countLadder++;

        Debug.Log("_countladder " + _countLadder);
        _isOnLadder = _countLadder > 0;
         if (_isOnLadder)
         {
             //_isOnLadder = true;
             animator.SetBool("On_Ladder", _isOnLadder);
             rb2d.isKinematic = true;

             float ladderMiddleX = ladder.transform.position.x;
             transform.position = new Vector3(ladderMiddleX, transform.position.y, transform.position.z);

            grounded = true;

             /*Hat _hat = this.GetComponentInChildren<Hat>();
             _hat.GetComponent<Renderer>().enabled = false;*/

            //GameObject.FindGameObjectWithTag("hat").GetComponent<Renderer>().enabled = false;
         }
    }

    public void OnStayLadder()
    {
        animator.SetBool("On_Ladder", true);
        grounded = true;
    }

    public void OnExitLadder()
    {
        _countLadder--;
        if (_countLadder < 0)
        {
            _countLadder = 0;
        }

        Debug.Log("_countladder " + _countLadder);
        _isOnLadder = _countLadder > 0;

        if (!_isOnLadder)
        {
            rb2d.isKinematic = false;
            grounded = false;

            animator.SetBool("On_Ladder", _isOnLadder);
            /*Hat _hat = this.GetComponentInChildren<Hat>();
            _hat.GetComponent<Renderer>().enabled = backward ? false : true;*/
            //GameObject.FindGameObjectWithTag("hat").GetComponent<Renderer>().enabled = backward ? false : true;
        }
    }

    public void MoveUp()
    {
        animator.SetBool("Ladder_Climb", true);
        //rb2d.AddForce((Vector2.up * speed));
        float currentY = transform.position.y;
        transform.position = new Vector3(transform.position.x, currentY + 0.05f, transform.position.z);
    }

    public void MoveDown()
    {
        animator.SetBool("Ladder_Climb", true);
        //rb2d.AddForce((Vector2.down * speed));
        float currentY = transform.position.y;
        transform.position = new Vector3(transform.position.x, currentY - 0.05f, transform.position.z);
    }
    #endregion Ladder

    /// <summary>
    /// Teleports the player to the given destination
    /// </summary>
    /// <param name="destination"></param>
    public void Teleport(Vector3 destination)
    {
        //transform.position = destination;
        transform.position = destination;
    }

    /// <summary>
    /// Resets the position of the player at its initial position
    /// TODO: Test after level change
    /// </summary>
    public void ResetPosition()
    {
        transform.position = initPosition;
    }

    public void OnEnterDoor(Door door)
    {
        //TODO: real door management
        if (!doorTaken)
        {
            FindObjectOfType<GameController>().LoadLevel(door.destination);
            doorTaken = true;
        }
    }

    void FixedUpdate()
    {
        _isOnLadder = _countLadder > 0;
        float h = Input.GetAxis("Horizontal");
        if(grounded)
        {
            animator.SetBool("Jump", false);
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        /*if (!_isOnLadder || (_isOnLadder && !grounded ))
        {*/
            if (h > 0)
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            if (grounded)
            {
                animator.SetTrigger("Walk");
            }
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
            if (grounded)
            {
                animator.SetTrigger("Walk");
            }
            if (!backward)
                {
                    Flip();
                }
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            if (grounded)
            {
                animator.SetTrigger("Rest");
            }
        }
       // }
    }

    private void Jump()
    {
        AudioClip currentClip = jumpClips[Random.Range(0, jumpClips.Length)];
        audioSource.clip = currentClip;
        audioSource.Play();
        animator.SetBool("Jump",true);
        /*if (_countLadder <= 0)
        {
            animator.SetBool("On_Ladder", false);
            animator.SetTrigger("Jump");
        }
        else
        {
            animator.SetBool("On_Ladder", true);
        }
        // We can jump while being on a Ladder, thus we need to get back to non kinematic
        bool isOnLadder = rb2d.isKinematic;
        if (isOnLadder)
        {
            rb2d.isKinematic = false;
        }*/
        rb2d.AddForce(Vector2.up * jumpPower);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            Spirit++;
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
            if (other.isActiveAndEnabled && (this.OnTop == null || (this.OnTop != null && !this.OnTop.Equals(other.GetComponent<Enemy>()))))
            {
                Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
                Damage(enemy.Damage, enemy.transform.position);
            }
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            Item item = other.gameObject.GetComponent<Item>();
            item.OnPlayerTouches();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.isActiveAndEnabled && this.OnTop != null && !this.OnTop.Equals(other.GetComponent<Enemy>()))
        {
            this.OnTop = null;
        }
    }
    public void Flip()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
        backward = !backward;
        /*Hat _hat = this.GetComponentInChildren<Hat>();
        _hat.GetComponent<Renderer>().enabled = backward ? false : true;*/
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

    public void Damage(int hit, Vector3 attacker)
    {
        
        if (!untouchable || easyKill)
        {
            becomesUntouchable();
           // Mental = Mental - hit;
            Health = Health - hit;
            //setMental(_mental);
            gameObject.GetComponent<Animation>().Play("RedFlash");
            
            Vector2 direction = new Vector2(attacker.x - transform.position.x, attacker.y - transform.position.y);
            direction.Normalize();
            rb2d.AddForce(new Vector2(direction.x * -knockback.x, direction.y * knockback.y));
         
        }
        if (Health <=0)
        {
            Die();
            
        }
    }

    /// <summary>
    /// Starts invincibility frames
    /// </summary>
    private void becomesUntouchable()
    {
        // Time in ms
        int untouchTime = 2000;
        untouchable = true;
        Timer t = new Timer(new TimerCallback(stopsBeingUntouchableCallback));
        t.Change(untouchTime, 0);
    }

    /// <summary>
    /// Stops invincibility frames (callback)
    /// </summary>
    /// <param name="state"></param>
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
        if(enemy.isdead)
        {
            if (easyKill)
            {
                Mental = Mental - 1000;
            }
            else
            {
                Mental = Mental - 10;
            }
        }
    }

    public void Die()
    {
    }
   
}
