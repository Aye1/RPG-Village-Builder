using UnityEngine;
using System.Collections;

public class DynamicClock : ShootingEnemy {

    public int debugHour = 1;
    private int _hour;

    public GameObject little;
    public GameObject big;

    public int Hour
    {
        get
        {
            return _hour;
        }
        set
        {
            if (value >= 1 && value <= 12 && value != _hour)
            {
                _hour = value;
                float angle = _hour / 12.0f * -360;
                little.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
	
	// Special update:
    // Update the position of minutes hand
	protected override void SpecialUpdate () {

        if (Hour != debugHour)
        {
            Hour = debugHour;
        }
        Vector3 direction = Target.position - transform.position;
        // The angle is always > 0, we add a sign depending on the relative position
        float sign = direction.x < 0 ? 1 : -1;
        float angle = sign * Vector3.Angle (Vector3.up, direction);
        big.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
