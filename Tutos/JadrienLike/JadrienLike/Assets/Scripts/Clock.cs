using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

    private Animator animator;
    public Transform target;
    public GameObject bullet;

    public Transform shootPoint;

    private int health;
    public int maxHealth;
    public int damage;

    private float distance;
    public float wakeRange;
    public float bulletSpeed;
    private float bulletTimer =0;
    public float shootInterval;

    private bool awake = false;
	

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        animator.SetBool("Awake", awake);
        RangeCheck();
    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= wakeRange)
        {
            awake = true;
        }
        else
        {
            awake = false;
        }

    }

    public void Attack()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            GameObject bulletClone;
            bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
            bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            bulletTimer = 0;

        }
    }
}
