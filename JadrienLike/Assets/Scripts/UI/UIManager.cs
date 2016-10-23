using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public DualityBar mentalBar;
    public DualityBar healthBar;
    public Player player;
    public GameController gameController;
    public Canvas pauseMenu;
    public Text spiritCount;
    public Inventory inventory;

    private BlackScreen _blackScreen;

    private static UIManager instance = null;
     

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _blackScreen = GetComponentInChildren<BlackScreen>();
        inventory.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if(mentalBar != null && player != null)
        {
            mentalBar.CurrentValue = player.Mental;
        }
        if(healthBar != null && healthBar != null && player != null)
        {
            healthBar.CurrentValue = player.Health;
        }
        if (spiritCount != null && player != null)
        {
            spiritCount.text = "Spirit: " + player.Spirit;
        }
        pauseMenu.enabled = gameController.pause;
        CheckInventoryChangeKey();
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

    private void CheckInventoryChangeKey()
    {
        if (Input.GetKeyDown(KeyCode.I) && !gameController.pause)
        {
            inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);   
        }
    }

    public bool ShouldPause()
    {
        return inventory.gameObject.activeSelf;
    }
}
