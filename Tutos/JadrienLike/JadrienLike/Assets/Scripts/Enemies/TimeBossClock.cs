using UnityEngine;
using System.Collections;
using System.Threading;

public class TimeBossClock : MonoBehaviour {

    public GameObject little;
    public GameObject big;
    private TimeBoss timeBoss;

    public float currentDirectionLittle = 1.0f;
    public float currentDirectionBig = 1.0f;
    
    private const float _angleStepLittle = 3.0f/360.0f;
    private const float _angleStepBig = 6.0f/360.0f;

    private bool _rotationInProgress = false;
    private bool _canLaunchRotation = false;
    private bool _canLaunchAttack = false;
    private bool _attackEnded = false;

    // Passed to true when the clock timer ends
    // so that the hand reaches its final position
    private bool _canStop = false;
    private bool _littleReachedTarget = false;
    private bool _bigReachedTarget = false;

    private int _hour = 1;
    private int _minute = 1;

    private int _rotationDuration = 5000;


    // Use this for initialization
	void Start () {
        _canLaunchRotation = true;
        timeBoss = GetComponentInChildren<TimeBoss>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if (_rotationInProgress)
        {
            float angleLittle = _angleStepLittle * -360 * currentDirectionLittle;
            float angleBig = _angleStepBig * - 360 * currentDirectionBig;

            // Reference vectors
            Vector3 targetHour = new Vector3(0.0f, 0.0f, (_hour-1)*30.0f);
            Vector3 targetMin = new Vector3(0.0f, 0.0f, (_minute-1)*30.0f);

            // Are the hands on their target?
            _littleReachedTarget = little.transform.rotation == Quaternion.Euler(targetHour);
            _bigReachedTarget = big.transform.rotation == Quaternion.Euler (targetMin);

            // Turn if you can't stop or until you reach your target
            if(!_littleReachedTarget || !_canStop)
            {
                little.transform.rotation = little.transform.rotation * Quaternion.AngleAxis(angleLittle, Vector3.forward);
            }
            if(!_bigReachedTarget || !_canStop)
            {
                big.transform.rotation = big.transform.rotation * Quaternion.AngleAxis(angleBig, Vector3.forward);
            }

            // Both hands have reached their target, start attack pattern
            if (_bigReachedTarget && _littleReachedTarget && _canStop)
            {
                _rotationInProgress = false;
                _canLaunchRotation = false;
                _canLaunchAttack = true;
            }
                      
        }

        if (_canLaunchAttack)
        {
            LaunchAttackPattern();
        }

        if(!timeBoss.patternInProgress && !_canLaunchAttack)
        {
            _canLaunchRotation = true;
        }

        if (_canLaunchRotation && !_rotationInProgress)
        {
            LaunchRotation();
        }
	}

    private void LaunchRotation()
    {
        _rotationInProgress = true;
        _canLaunchRotation = false;
        _canStop = false;
        _littleReachedTarget = false;
        _bigReachedTarget = false;

        // Define target hour and minutes
        _hour = Random.Range(1,12);
        _minute = Random.Range(1,12);

        // Rotation direction
        currentDirectionLittle = Random.Range(0.0f, 1.0f) < 0.5f ? -1.0f : 1.0f;
        currentDirectionBig = Random.Range(0.0f, 1.0f) < 0.5f ? -1.0f : 1.0f;

        // Launch rotation
        Timer t = new Timer(LaunchRotationCallback);
        t.Change(_rotationDuration, 0);
    }

    public void LaunchRotationCallback(object state)
    {
        _canStop = true;
    }

    private void LaunchAttackPattern()
    {
        _canLaunchAttack = false;
        if (timeBoss != null)
        {
            //timeBoss.transform.Rotate(0.0f, 0.0f, 90.0f);
            timeBoss.LaunchPattern(2, 1);
            /*
             * Stuff happens
             */
            /*Timer t = new Timer(DummyCallback);
            t.Change(5000, 0);*/
        }
    }

    /*public void DummyCallback(object state)
    {
        _canLaunchRotation = true;
    }*/
}
