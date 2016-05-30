using UnityEngine;
using System.Collections;
using System;

public class Deadline : DumbEnemy {



    public float moveSpeed = 0.1f;
    private Vector2 moveAmount = new Vector2(0, 1);
    public float moveDirection = 1.0f;

    public override void Move()
    {
        moveAmount.y = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(moveAmount); 

        if (!groundAhead)
        {
            moveDirection *= -1;
            Vector3 enemyScale = transform.localScale;
            enemyScale.y *= -1;
            transform.localScale = enemyScale;
        }
    }
}
