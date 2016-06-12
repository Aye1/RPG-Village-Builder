using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player player;
	public BoardManager boardManager;

	private Board currentBoard = null;
	private GameController instance = null;

    public bool pause;

	void Awake () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);

		currentBoard = new Board();
		MapLoader loader = new MapLoader("tuto_map");
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
			//ArrayList firstLayer = parsedLayers.ToArray()[0] as ArrayList;
			currentBoard.Layers = parsedLayers;
			Debug.Log ("Layers added to the board");
			//currentBoard.SizeY = firstLayer.Count;
			//ArrayList firstRow = firstLayer.ToArray()[0] as ArrayList;
			//currentBoard.SizeX = firstRow.Count;
			//firstLayer.Insert(0, firstRow);
			boardManager.InstantiateBoard(currentBoard);
		}
        player.transform.position = boardManager.InitPlayerPosition;
        player.initPosition = boardManager.InitPlayerPosition;
	}

	// Update is called once per frame
	void Update () {
        if (boardManager.debugMode)
        {
            boardManager.ZoneId = boardManager.debugZoneId;
        }
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
}
