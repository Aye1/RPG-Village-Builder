using System;
using UnityEngine;

public abstract class DumbEnemy : Enemy {
    public override void Attack() {}
    protected override void Init() {}
    public override void OnHit() {}
}
