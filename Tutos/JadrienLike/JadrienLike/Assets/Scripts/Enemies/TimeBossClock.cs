using UnityEngine;
using System.Collections;
using System.Threading;

public class TimeBossClock : MonoBehaviour {

    public GameObject little;
    public GameObject big;
    private TimeBoss timeBoss;

    public float currentDirectionLittle = 1.0f;
    public float currentDirectionBig = 1.0f;
    
    private const float _angleStep = 4.0f/360.0f;
    private bool _rotationInProgress = false;
    private bool _canLaunchRotation = false;
    private bool _canLaunchAttack = false;


    // Use this for initialization
	void Start () {
        _canLaunchRotation = true;
        timeBoss = GetComponentInChildren<TimeBoss>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if (_rotationInProgress)
        {
            float angleLittle = _angleStep * -360 * currentDirectionLittle;
            float angleBig = _angleStep * - 360 * currentDirectionBig;
            little.transform.rotation = little.transform.rotation * Quaternion.AngleAxis(angleLittle, Vector3.forward);
            big.transform.rotation = big.transform.rotation * Quaternion.AngleAxis(angleBig, Vector3.forward);
        }

        if (_canLaunchRotation && !_rotationInProgress)
        {
            LaunchRotation();
        }
        if (_canLaunchAttack)
        {
            LaunchAttackPattern();
        }
	}

    private void LaunchRotation()
    {
        _rotationInProgress = true;
        _canLaunchRotation = false;

        float directionLittle = 1.0f;
        float directionBig = 1.0f;

        currentDirectionLittle = directionLittle;
        currentDirectionBig = directionBig;

        Timer t = new Timer(LaunchRotationCallback);
        t.Change(5000, 0);
    }

    public void LaunchRotationCallback(object state)
    {
        _rotationInProgress = false;
        _canLaunchAttack = true;
    }

    private void LaunchAttackPattern()
    {
        _canLaunchAttack = false;
        if (timeBoss != null)
        {
            timeBoss.transform.Rotate(0.0f, 0.0f, 90.0f);
            /*
             * Stuff happens
             */
            Timer t = new Timer(DummyCallback);
            t.Change(10000, 0);
        }
    }

    public void DummyCallback(object state)
    {
        _canLaunchRotation = true;
    }
}
