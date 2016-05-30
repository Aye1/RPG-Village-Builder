using UnityEngine;
using System.Collections;

public class Clock : Enemy {

    // Ony for Unity setting
    public int initMaxHealth = 10;
    public int initDamage = 3;
    public int initWakeRange = 5;
    public int initShootInterval = 1;

    protected override void Init()
    {
        MaxHealth = initMaxHealth;
        Damage = initDamage;
        WakeRange = initWakeRange;
        ShootInterval = initShootInterval;
    }
  
    public override void Attack()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= ShootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            Bullet bulletClone;
            bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as Bullet;
            bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bullet.bulletSpeed;

            bulletTimer = 0;

        }
    }
}
