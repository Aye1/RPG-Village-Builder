using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class TimeBoss : Enemy
{

    #region Attack
    public Bullet[] bullet;
    private float ShootInterval = 1f;
    public Vector3 shootPoint;
    private ArrayList _direction = new ArrayList();
    private float scale = 1.0f; //scale of the bullet
    private int _numberBullet = 1;
    private bool AttackEnded = true;
    #endregion

    private int _hour;
    private int _minute;
    private Vector3 _targetPosition;
    private Vector3 _clockPosition;
    private bool _targetReached;
    public bool patternInProgress;

    private int _step;

    #region Pattern 1
    private int _stepNumberPattern1 = 4;
    private Vector3 _pattern1Pos1 = new Vector3(6.5f, 0.0f, 0.0f);
    private Vector3 _pattern1Pos2 = new Vector3(0.0f, -6.0f, 0.0f);
    private Vector3 _pattern1Pos3 = new Vector3(-6.5f, 0.0f, 0.0f);

    #endregion

    #region Pattern 2
    private int _stepNumberPattern2 = 3;
    private Vector3 _pattern2Pos1 = new Vector3(6.5f, 0.0f, 0.0f);
    private Vector3 _pattern2Pos2 = new Vector3(-6.5f, 0.0f, 0.0f);
    #endregion


    #region Accessors
    public int Hour
    {
        get
        {
            return _hour;
        }
        set
        {
            if (value >= 1 && value <= 12)
            {
                _hour = value;
            }
        }
    }

    public int Minute
    {
        get
        {
            return _minute;
        }
        set
        {
            if (value >= 1 && value <= 12)
            {
                _minute = value;
            }
        }
    }
    #endregion

    protected override void Init()
    {
        _targetPosition = new Vector3(0.0f, 0.0f, 0.0f);
        _clockPosition = GetComponentInParent<TimeBossClock>().transform.position;
        _targetPosition = _clockPosition;
        InitPatternPosition();
        _moveSpeed = 0.1f;
        shootPoint = _clockPosition;
        patternInProgress = false;
    }

    private void InitPatternPosition()
    {
        _pattern1Pos1 = _pattern1Pos1 + _clockPosition;
        _pattern1Pos2 = _pattern1Pos2 + _clockPosition;
        _pattern1Pos3 = _pattern1Pos3 + _clockPosition;
        _pattern2Pos1 = _pattern2Pos1 + _clockPosition;
        _pattern2Pos2 = _pattern2Pos2 + _clockPosition;
    }

    public void LaunchPattern(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
        _step = 0;
        _targetReached = true;
        patternInProgress = true;
        AttackEnded = false;
    }

    public override void Move()
    {
        float dist = Vector3.Distance(_targetPosition, transform.position);
        if (dist <= 0.1f)
        {
            _targetReached = true;
            transform.position = _targetPosition;
        }
        else
        {
            Vector3 move = _targetPosition - transform.position;
            move.Normalize();
            transform.Translate(move * MoveSpeed);
        }
    }


    protected override void SpecialUpdate()
    {
        if (_targetReached && patternInProgress)
        {
            switch (Hour)
            {
                case 1:
                    UpdatePattern1();
                    break;
                case 2:
                    UpdatePattern2();
                    break;
                case 3:
                    UpdatePattern3();
                    break;
                case 4:
                    UpdatePattern4();
                    break;
            }
        }
        if (patternInProgress)
        {
            scale = _minute / 12.0f * 5.0f;
            switch (Hour)
            {
                case 1:
                    _numberBullet = 0;
                    _direction.Clear();
                    shootPoint = _clockPosition;
                    break;
                case 2:
                    _numberBullet = 1;
                    if(_direction.Count <=0)
                    {
                        _direction.Add(Vector2.down);
                    }
                    ShootInterval = 0.5f;
                    //scale = 5.0f;
                    shootPoint = gameObject.transform.position;
                    break;
                case 3:
                    _numberBullet = 8;
                    //scale = 3.0f;
                    ShootInterval = 0.0f;
                    if (_direction.Count <=0)
                    {
                        FillDirectionInCircle(_numberBullet);
                    }
                    shootPoint = _clockPosition;
                    Attack();
                    AttackEnded = true;
                    break;
                case 4:
                    _numberBullet = 16;
                    //scale = 1.0f;
                    ShootInterval = 0.0f;
                    if (_direction.Count <=0)
                    {
                        FillDirectionInCircle(_numberBullet);
                    }
                    shootPoint = _pattern1Pos1;
                    Attack();
                    shootPoint = _pattern1Pos3;
                    Attack();
                    AttackEnded = true;
                    break;
            }
            Attack();
        }
    }

   
    //Fill the array of direction with #numberbullet vectors, along a circle
    private void FillDirectionInCircle(int numberBullet)
    {
        float increment = 2 * Mathf.PI / numberBullet;
        for (int i = 0; i < numberBullet; i++)
        {
            _direction.Add(new Vector2(Mathf.Cos(i * increment), Mathf.Sin(i * increment)));
        }
    }
    private void UpdatePattern1()
    {
        if (_step != _stepNumberPattern1)
        {
            _step++;
            _targetReached = false;
            switch (_step)
            {
                case 1:
                    _targetPosition = _pattern1Pos1;
                    break;
                case 2:
                    _targetPosition = _pattern1Pos2;
                    break;
                case 3:
                    _targetPosition = _pattern1Pos3;
                    break;
                case 4:
                    _targetPosition = _clockPosition;
                    break;
            }
        }
        else
        {
            patternInProgress = false;
            AttackEnded = true;
            if(_direction.Count > 0)
            {
                _direction.Clear();
            }
        }
    }
    private void UpdatePattern2()
    {
        if (_step != _stepNumberPattern2)
        {
            _step++;
            _targetReached = false;
            switch (_step)
            {
                case 1:
                    _targetPosition = _pattern2Pos1;
                    break;
                case 2:
                    _targetPosition = _pattern2Pos2;
                    break;
                case 3:
                    _targetPosition = _clockPosition;
                    break;
            }
        }
        else
        {
            patternInProgress = false;
            AttackEnded = true;
            _direction.Clear();
        }
    }
    private void UpdatePattern3()
    {
        WaitABit();
        /*if(AttackEnded)
        {
            patternInProgress = false;
            _direction.Clear();
        }*/
    }

    private void UpdatePattern4()
    {
        WaitABit();
        /*if (AttackEnded)
        {
            patternInProgress = false;
            _direction.Clear();
        }*/
    }

    public override void Attack()
    {
        _internalTimer += Time.deltaTime;
        if (_internalTimer >= ShootInterval && _direction.Count > 0 && !AttackEnded)
        {
            foreach(Vector2 vec in _direction)
            {
                Bullet bulletClone;
                //bulletClone = Instantiate(bullet[Minute - 1], shootPoint, Quaternion.identity) as Bullet;
                bulletClone = Instantiate(bullet[0], shootPoint, Quaternion.identity) as Bullet;
                bulletClone.GetComponent<Rigidbody2D>().velocity = vec * bulletClone.bulletSpeed;
                bulletClone.Scale(scale);
                if (ShootInterval != 0.0f)
                {
                    _internalTimer = 0;
                }
                /*else { 
                    AttackEnded = true;
                }*/
            }
        }
    }

    public override void OnHit()
    {
    }

    private void WaitABit()
    {
        int time = 2000;
        Timer t = new Timer(WaitABitCallback);
        t.Change(time, 0);
    }

    public void WaitABitCallback(object state)
    {
        if (AttackEnded)
        {
            patternInProgress = false;
            _direction.Clear();
        }
    }
}
