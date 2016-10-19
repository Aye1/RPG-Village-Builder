using System;
using UnityEngine;

public abstract class DumbEnemy : Enemy {
    public override void Attack() {}
    protected override void Init() {}
    protected override void SpecialUpdate() {}
    public override void OnHit()
    {
        gameObject.GetComponent<Animation>().Play("RedFlash");
    }
}
