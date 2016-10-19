using UnityEngine;
using System.Collections;
using System;

public class DumbClock : ShootingEnemy {

    public override void Attack()
    {
       
            Bullet bulletClone_up;
            bulletClone_up = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as Bullet;
            bulletClone_up.GetComponent<Rigidbody2D>().velocity = Vector2.up * bullet.bulletSpeed;

            Bullet bulletClone_down;
            bulletClone_down = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as Bullet;
            bulletClone_down.GetComponent<Rigidbody2D>().velocity = Vector2.down * bullet.bulletSpeed;

            Bullet bulletClone_left;
            bulletClone_left = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as Bullet;
            bulletClone_left.GetComponent<Rigidbody2D>().velocity = Vector2.left * bullet.bulletSpeed;

            Bullet bulletClone_right;
            bulletClone_right = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as Bullet;
            bulletClone_right.GetComponent<Rigidbody2D>().velocity = Vector2.right * bullet.bulletSpeed;

        _internalTimer = 0;
    }

    public void FixedUpdate()
    {
        if (!debugCanShoot)
            return;
        _internalTimer += Time.deltaTime;
        if (_internalTimer >= ShootInterval)
        {
            Attack();
        }
    }
}
