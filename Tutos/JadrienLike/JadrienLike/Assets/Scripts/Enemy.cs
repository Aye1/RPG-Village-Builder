using UnityEngine;
using System.Collections;

public abstract class Enemy :MonoBehaviour  {

    // Set by Unity
    protected Animator animator;
    public Transform target;
    public Bullet bullet;
    public Transform shootPoint;

    protected int health;
    protected int _maxHealth;
    protected int _damage;

    private float distance;
    protected float _wakeRange;

    // TODO: move to the Bullet object
  
    protected float bulletTimer = 0;
    protected float _shootInterval;

    protected bool awake = false;

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
    /// Accessor for the time between two shoots
    /// </summary>
    public float ShootInterval
    {
        get
        {
            return _shootInterval;
        }
        set
        {
            if (value >= 0)
            {
                _shootInterval = value;
            }
        }
    }

    #endregion

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        health = MaxHealth;
        Init();
    }

    protected abstract void Init();

    void Update()
    {
        RangeCheck();
        animator.SetBool("Awake", awake);
    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= WakeRange)
        {
            awake = true;
        }
        else
        {
            awake = false;
        }
    }

    public abstract void Attack();
}
