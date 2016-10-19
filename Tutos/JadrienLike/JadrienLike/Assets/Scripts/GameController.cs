using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Player player;
	public BoardManager boardManager;
    private RoomManager _roomManager;
    public UIManager uiManager;
	private static GameController instance = null;

    public bool pause;

    // Used for thread issues
    private bool _canSwitchToNightmare;

	void Awake () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
        if (player == null)
        {
            player = GetComponent<Player>();
        }
        _roomManager = GetComponent<RoomManager>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(boardManager);
        DontDestroyOnLoad(uiManager);
        DontDestroyOnLoad(uiManager.pauseMenu);

		//currentBoard = new Board();
        LoadLevel("Rooms/room_132_ini");
	}

    public void LoadLevel(string levelName)
    {
        boardManager.EmptyBoard();
        // RoomManager may not be initialized before being used, for some reason
        _roomManager.Init();
        _roomManager.LoadRoom(levelName);

        boardManager.ZoneId = 0;
        player.transform.position = boardManager.InitPlayerPosition;
        player.initPosition = boardManager.InitPlayerPosition;
        _roomManager.canStartChecking = true;
    }

	// Update is called once per frame
	void Update () {
        if(uiManager == null)
        {
            uiManager = GetComponent<UIManager>();
        }
        if (boardManager.debugMode)
        {
            boardManager.ZoneId = boardManager.debugZoneId;
        }
        ManagePause();
        CheckPlayerMental();
        if(_canSwitchToNightmare)
        {
            _canSwitchToNightmare = false;
            int nightmareId = 1;
            boardManager.ZoneId = nightmareId;
        }
	}

    /// <summary>
    /// Checks the player mental and launches the nightmare
    /// animation and zone change if needed
    /// </summary>
    private void CheckPlayerMental()
    {
        // TODO: nightmareId computed dynamically
        int nightmareId = 1;
        if (player.Mental == 0 && boardManager.ZoneId != nightmareId)
        {
            // Block the game during the change
            Time.timeScale = 0;
            uiManager.LaunchBlackScreenTransition();
            Timer t = new Timer(CheckPlayerMentalCallback);
            t.Change (2400, 0);
        }
    }

    /// <summary>
    /// Callback called after a delay when the player enters in nightmare mode
    /// It allows the screen to turn black before changing the layout
    /// </summary>
    /// <param name="state">State.</param>
    private void CheckPlayerMentalCallback(object state)
    {
        // Tell the controller that he can change the zone layout at the next update
        _canSwitchToNightmare = true;
    }

    private void ManagePause()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
        }
        if(pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Resumes the game when it's in pause.
    /// </summary>
    public void ResumeGame()
    {
        pause = false;
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Resets the player position to its initial position.
    /// </summary>
    public void ResetPlayerPos()
    {
        player.transform.position = boardManager.InitPlayerPosition;
    }

    public void BackToMainMenu()
    {
        //Application.LoadLevel(0);
        pause = false;
        HideUI();
        HidePlayer();
        SceneManager.LoadScene(0);
    }

    public void HideUI()
    {
        uiManager.gameObject.SetActive(false);
        uiManager.pauseMenu.gameObject.SetActive(false);
    }

    public void DisplayUI()
    {
        uiManager.gameObject.SetActive(true);
        uiManager.pauseMenu.gameObject.SetActive(true);
    }

    public void HidePlayer()
    {
        player.gameObject.SetActive(false);
    }

    public void DisplayPlayer()
    {
        player.gameObject.SetActive(true);
    }
}
