using UnityEngine;
using System.Collections;
using System.Threading;

public class GameController : MonoBehaviour {

	public Player player;
	public BoardManager boardManager;
    public UIManager uiManager;

	private Board currentBoard = null;
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

		DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(player);
        //DontDestroyOnLoad(uiManager);

		currentBoard = new Board();
		MapLoader loader = new MapLoader("bossmap");
		ArrayList layers = loader.Layers;
		ArrayList parsedLayers = new ArrayList();
		CSVParser parser = CSVParser.Instance;

		foreach (string layer in layers) 
		{
			parsedLayers.Add(CSVParser.ParseCSV(layer));
		}
		if (parsedLayers.Count == 0)
			Debug.Log("Error while loading the map.");
		else 
		{
			currentBoard.Layers = parsedLayers;
			Debug.Log ("Layers added to the board");
			boardManager.InstantiateBoard(currentBoard);
            boardManager.ZoneId = 0;
		}
        player.transform.position = boardManager.InitPlayerPosition;
        player.initPosition = boardManager.InitPlayerPosition;
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
            t.Change (3900, 0);
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
        Application.LoadLevel(0);
    }
}
