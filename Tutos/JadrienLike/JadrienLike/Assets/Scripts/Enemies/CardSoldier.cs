using UnityEngine;
using System;

public class CardSoldier : MeleeEnemy {

    public override void Attack()
    {
    }

    public override void Move()
    {
        Vector2 direction = Target.transform.position - transform.position;
        float distanceToPlayer = Vector3.Distance(Target.transform.position, transform.position);
        direction.Normalize();
        if((IsPlayerAhead() || _isChasingPlayer)
            && distanceToPlayer <= AggroRange)
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
            //to change : wrong animation
            gameObject.GetComponent<Animation>().Play("BlueFlash");
            moveDirection = direction.x >= 0 ? 1.0f : -1.0f;
            currentMoveSpeed = MoveSpeed * 5.0f;
        }
        else
        {
            if (!groundAhead)
            {
                moveDirection *= -1;
                Flip();
            }
            currentMoveSpeed = MoveSpeed;
        }
        moveAmount.x = moveDirection * currentMoveSpeed * Time.deltaTime;
        transform.Translate(moveAmount);
    }
    public override void OnHit()
    {
        gameObject.GetComponent<Animation>().Play("RedFlash");
        _isChasingPlayer = true;
        if (!IsPlayerAhead())
        {
            Flip();
        }
    }

    protected override void Init()
    {
        AttackPeriod = initAttackPeriod;   
    }
}
