using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject player;
	public BoardManager boardManager;

	private Board currentBoard = null;
	private GameController instance = null;

	void Awake () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);

		currentBoard = new Board();
		MapLoader loader = new MapLoader("map_csv");
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
	}

	// Update is called once per frame
	void Update () {
	
	}
}
