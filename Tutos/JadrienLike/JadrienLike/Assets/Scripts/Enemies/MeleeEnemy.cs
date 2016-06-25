using UnityEngine;
using System.Collections;

public abstract class MeleeEnemy : Enemy {

    public float initAttackPeriod;
    private float _attackPeriod;

    #region Accessors
    public float AttackPeriod
    {
        get
        {
            return _attackPeriod;
        }
        set
        {
            if (value >= 0)
            {
                _attackPeriod = value;
            }
        }
    }
    #endregion

    protected override void SpecialUpdate() {}
}
