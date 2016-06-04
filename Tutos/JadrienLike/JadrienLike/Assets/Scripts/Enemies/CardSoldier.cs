using UnityEngine;
using System;

public class CardSoldier : MeleeEnemy {

    private float moveDirection = 1.0f;

    public override void Attack()
    {
    }

    public override void Move()
    {
        Vector2 direction = Target.transform.position - transform.position;
        float distanceToPlayer = Vector3.Distance(Target.transform.position, transform.position);
        direction.Normalize();
        if(direction.x * moveDirection > 0 && distanceToPlayer <= AggroRange)
        {
            _isChasingPlayer = true;
        } 
        else
        {
            _isChasingPlayer = false;
        }

        Vector2 moveAmount = new Vector2(0, 0);
        float currentMoveSpeed;

        if (IsChasingPlayer)
        {
            moveDirection = direction.x >= 0 ? 1.0f : -1.0f;
            currentMoveSpeed = MoveSpeed * 5.0f;
        }
        else
        {
            if (!groundAhead)
            {
                moveDirection *= -1;
                Vector3 enemyScale = transform.localScale;
                enemyScale.x *= -1;
                transform.localScale = enemyScale;
            }
            currentMoveSpeed = MoveSpeed;
        }
        moveAmount.x = moveDirection * currentMoveSpeed * Time.deltaTime;
        transform.Translate(moveAmount);
    }

    protected override void Init()
    {
        AttackPeriod = initAttackPeriod;   
    }
}
