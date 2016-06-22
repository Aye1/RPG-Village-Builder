using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using System;

public class UIManager : MonoBehaviour {

    public DualityBar mentalBar;
    public Player player;
    public GameController gameController;
    public Canvas pauseMenu;

    private BlackScreen _blackScreen;
     

	// Use this for initialization
	void Start () {
        _blackScreen = GetComponentInChildren<BlackScreen>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(mentalBar != null && player != null)
        {
            mentalBar.CurrentValue = player.Mental;
        }
        pauseMenu.enabled = gameController.pause;

	}

    public void LaunchBlackScreenTransition()
    {
        if (_blackScreen != null)
        {
            _blackScreen.LaunchTransition();
        }
        else
        {
            Debug.Log("Black screen not found");
        }
    }

}
