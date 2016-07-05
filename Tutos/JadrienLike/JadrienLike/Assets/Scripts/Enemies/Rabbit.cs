using UnityEngine;

public class Rabbit : DumbEnemy {

    private Vector2 moveAmount = new Vector2(1, 0);

    public override void Move()
    {
        moveAmount.x = moveDirection * MoveSpeed * Time.deltaTime;
        transform.Translate(moveAmount); 

        if (!groundAhead)
        {
            moveDirection *= -1;
            Flip();
        }
    }
}
