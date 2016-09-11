using UnityEngine;

public abstract class Enemy : MonoBehaviour  {

    // Set by Unity
    protected Animator animator;
    protected Transform _target;

    public int health; //to change
    protected int _maxHealth;
    protected int _damage;
    protected float _moveSpeed;
    protected bool _isChasingPlayer = false;
    protected float _aggroRange;
    public bool isdead = false;
    protected float _internalTimer = 0;
    protected float _wakeRange;
    protected ParticleSystem _bloodEmitter;

    protected float moveDirection = 1.0f;

    protected bool awake = false;

    public bool groundAhead = false;

    // Ony for Unity setting
    public int initMaxHealth = 10;
    public int initDamage = 3;
    public int initWakeRange = 5;
    public float initMoveSpeed = 1.0f;
    public float initAggroRange = 3;


    #region Accessor

    /// <summary>
    /// Accessor for the max health of the enemy
    /// </summary>
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if(value > 0)
            {
                _maxHealth = value;
            }
        }
    }

    /// <summary>
    /// Accessor for the damage dealt by the enemy
    /// </summary>
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            if (value >= 0)
            {
                _damage = value;
            }
        }
    }

    /// <summary>
    /// Accessor for the awakening range
    /// </summary>
    public float WakeRange
    {
        get
        {
            return _wakeRange;
        }
        set
        {
            if (value >= 0)
            {
                _wakeRange = value;
            }
        }
    }

    /// <summary>
    /// Accessor for the transform of the target player
    /// </summary>
    public Transform Target
    {
        get
        {
            return _target;
        }
        set
        {
            if (value != null)
            {
                _target = value;
            }
        }
    }

    /// <summary>
    /// Accessor for the move speed.
    /// </summary>
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }

        set
        {
            if (value >=0)
            {
                _moveSpeed = value;
            }
        }
    }

    public bool IsChasingPlayer
    {
        get
        {
            return _isChasingPlayer;
        }
    }

    public float AggroRange
    {
        get
        {
            return _aggroRange;
        }
        set
        {
            if (value > 0)
            {
                _aggroRange = value;
            }
        }
    }

    #endregion

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        Player player = FindObjectsOfType(typeof(Player))[0] as Player;
        Target = player.transform;
        _bloodEmitter = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        MaxHealth = initMaxHealth;
        health = MaxHealth;
        Damage = initDamage;
        WakeRange = initWakeRange;
        MoveSpeed = initMoveSpeed;
        AggroRange = initAggroRange;
        //dropManager = FindObjectOfType<DropManager>(); //for the drop
        Init();
    }

    void Update()
    {
        SpecialUpdate();
        RangeCheck();
        if (animator != null)
        {
            animator.SetBool("Awake", awake);
        }
        Move();
    }

    void RangeCheck()
    {
        float distance = Vector3.Distance(transform.position, Target.transform.position);
        if (distance <= WakeRange)
        {
            awake = true;
        }
        else
        {
            awake = false;
        }
    }

    public void Flip()
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }

    public bool IsPlayerAhead()
    {
        Vector2 direction = Target.transform.position - transform.position;
        return direction.x * moveDirection > 0;
    }

    public void OnHurt(int hit)
    {
        if (_bloodEmitter != null)
        {
            _bloodEmitter.Play();
        }
        health -= hit;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isdead = true;
        DropManager.Instance.CreateSphere(gameObject.transform.position);
        gameObject.SetActive(false);
    }
    #region Abstract methods
    // Init variables specific to the enemy
    protected abstract void Init();
    // Launch the enemy attack
    public abstract void Attack();
    // Move the enemy
    public abstract void Move();
    // When the Enemy is hit
    public abstract void OnHit();
    // The enemy has specific updates to do
    protected abstract void SpecialUpdate();
    #endregion
}
