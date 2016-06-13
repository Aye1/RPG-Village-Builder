using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public DualityBar mentalBar;
    public Player player;
    public GameController gameController;
    public Canvas pauseMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(mentalBar != null && player != null)
        {
            mentalBar.CurrentValue = player.Mental;
        }
        pauseMenu.enabled = gameController.pause;
	}
}
