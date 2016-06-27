using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCd = 0.3f;

    public Collider2D attacktrigger;
    private Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        attacktrigger.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("f") && !attacking)
        {
            attacking = true;
            attackTimer = attackCd;
            attacktrigger.enabled = true;
        }
        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attacktrigger.enabled = false;
            }
        }
        animator.SetBool("Attacking", attacking);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && attacktrigger.enabled)
        {
           Player player = this.GetComponentInParent<Player>();
            player.Attack(other.GetComponent<Enemy>(), player.weaponDamage);
        }
        if (other.gameObject.CompareTag("Destructible"))
        {
            Destructible destObj = (Destructible) other.GetComponent<Destructible>();
            destObj.OnHit();
        }
    }
}
