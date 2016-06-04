using UnityEngine;

public class Deadline : DumbEnemy {

    private Vector2 moveAmount = new Vector2(1, 0);
    public float moveDirection = 1.0f;

    public override void Move()
    {
        moveAmount.x = moveDirection * MoveSpeed * Time.deltaTime;
        transform.Translate(moveAmount); 

        if (!groundAhead)
        {
            moveDirection *= -1;
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
        }
    }
}
