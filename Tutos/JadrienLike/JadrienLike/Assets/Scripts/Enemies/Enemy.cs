using UnityEngine;
using System.Collections;

public abstract class Enemy :MonoBehaviour  {

    // Set by Unity
    protected Animator animator;
    protected Transform _target;

    protected int health;
    protected int _maxHealth;
    protected int _damage;

    private float distance;
    protected float _wakeRange;

    protected bool awake = false;

    public bool groundAhead = false;


    // Ony for Unity setting
    public int initMaxHealth = 10;
    public int initDamage = 3;
    public int initWakeRange = 5;


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

    #endregion

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        Player player = FindObjectsOfType(typeof(Player))[0] as Player;
        Target = player.transform;
    }

    void Start()
    {
        health = MaxHealth;
        MaxHealth = initMaxHealth;
        Damage = initDamage;
        WakeRange = initWakeRange;
        Init();
    }

    void Update()
    {
        RangeCheck();
        if (animator != null)
        {
            animator.SetBool("Awake", awake);
        }
        Move();
    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, Target.transform.position);
        if (distance <= WakeRange)
        {
            awake = true;
        }
        else
        {
            awake = false;
        }
    }

    #region Abstract methods
    // Init variables specific to the enemy
    protected abstract void Init();
    // Launch the enemy attack
    public abstract void Attack();
    // Move the enemy
    public abstract void Move();
    #endregion
}
