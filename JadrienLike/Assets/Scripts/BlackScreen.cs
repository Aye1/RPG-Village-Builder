using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class BlackScreen : MonoBehaviour {

    // Fading time in s
    private float _fadingtime = 1f;
    // Black screen time includes fade in time (in s) + complete black time (in ms)
    private int _blackScreenTime = 2500;
    private Image _blackScreen;
    private Text _nightmareText;
    // Used to avoid thread issues
    private bool _canBlackScreenFadeOut;
    private bool _isLaunchingBlackScreen;

    #region Unity debug
    public bool debugLaunchTransition;
    #endregion

	// Use this for initialization
	void Start () {
        _blackScreen = GetComponentInParent<Image>();
        _nightmareText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(_canBlackScreenFadeOut)
        {
            _canBlackScreenFadeOut = false;
            _blackScreen.CrossFadeAlpha(0.0f, _fadingtime, true);
            _nightmareText.CrossFadeAlpha(0.0f, _fadingtime, true);
        }
        if (debugLaunchTransition)
        {
            debugLaunchTransition = false;
            LaunchTransition();
        }
	}

    public void LaunchTransition()
    {
        if (!_isLaunchingBlackScreen)
        {
            _isLaunchingBlackScreen = true;
            _blackScreen.canvasRenderer.SetAlpha(0.01f);
            _blackScreen.CrossFadeAlpha(255, _fadingtime, true);
            _nightmareText.canvasRenderer.SetAlpha(0.01f);
            _nightmareText.CrossFadeAlpha(255, _fadingtime, true);
            Timer t = new Timer(new TimerCallback(LaunchTransitionCallback));
            t.Change(_blackScreenTime, 0);
        }
    }
    
    private void LaunchTransitionCallback(object state)
    {
        _canBlackScreenFadeOut = true;
        _isLaunchingBlackScreen = false;
    }
}
