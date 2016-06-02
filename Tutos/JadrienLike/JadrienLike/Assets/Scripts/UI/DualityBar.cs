using UnityEngine;
using UnityEngine.UI;

public class DualityBar : MonoBehaviour {

    #region Unity debug
    public bool debugMode;
    public int debugValue;
    public int debugMargin;
    #endregion

    #region Private fields
    private int _minValue = 0;
    private int _maxValue = 100;
    private int _currentValue = 50;
    private int _margin = 7;
    #endregion

    #region Accessors
    public int MinValue
    {
        get
        {
            return _minValue;
        }
        set
        {
            if(value < _maxValue)
            {
                _minValue = value;
            }
        }
    }

    public int MaxValue
    {
        get
        {
            return _maxValue;
        }
        set
        {
            if (value > _minValue)
            {
                _maxValue = value;
            }
        }
    }

    /// <summary>
    /// Accessor to the margin. This margin is applied 
    /// on both sides of the bar so that the cursor
    /// does not go out the bar.
    /// </summary>
    public int Margin
    {
        get
        {
            return _margin;
        }
        set
        {
            if (value < 50)
            {
                _margin = value;
            }
        }
    }

    public int CurrentValue
    {
        get
        {
            return _currentValue;
        }
        set
        {
            if (value < _minValue)
            {
                _currentValue = _minValue;
            }
            else if (_maxValue < value)
            {
                _currentValue = _maxValue;
            }
            else
            {
                _currentValue = value;
            }
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        // Debug using Unity
        int usedValue = debugMode ? debugValue : _currentValue;
        int usedMargin = debugMode ? debugMargin : _margin;

        // It seems that the picture goes from -50 to 50
        int offsetX = -50;
        // The first element is the bar itself, counted as its own child
        Image cursor = gameObject.GetComponentsInChildren<Image>()[1];
        float ratio = usedValue / (float)(_maxValue - _minValue);
        float localX = (100 - 2 * usedMargin) * ratio + offsetX + usedMargin;
        cursor.transform.localPosition = new Vector3(localX, 0.0f, 0.0f);
	}
}
