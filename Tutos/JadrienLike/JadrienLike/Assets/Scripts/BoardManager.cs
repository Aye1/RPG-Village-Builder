using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System;

public class BoardManager : MonoBehaviour {

    #region Tiles
    public GameObject[] topTiles;
	public GameObject[] bottomTiles;
	public GameObject[] leftTiles;
	public GameObject[] rightTiles;
	public GameObject[] fullTiles;
	public GameObject[] cornerbgTiles;
	public GameObject[] cornerbdTiles;
	public GameObject[] cornerhgTiles;
	public GameObject[] cornerhdTiles;
	public GameObject[] cliffbgTiles;
	public GameObject[] cliffbdTiles;
	public GameObject[] cliffhgTiles;
	public GameObject[] cliffhdTiles;
	public GameObject[] backgroundTiles;
    public GameObject[] exitTiles;
    #endregion

    #region Private variables
    private Vector3 _initPlayerPosition;

    private int minX = 20;
	private int maxX = 40;
	private int minY = 20;
	private int maxY = 40;
    #endregion

    private Transform boardHolder;

    #region Accessors
    public Vector3 InitPlayerPosition
    {
        get
        {
            return _initPlayerPosition;
        }
        set
        {
            if (value != null)
            {
                _initPlayerPosition = value;
            }
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Board CreateBoard() {
        // First line may be removed
		IntCouple boardsize = GenerateBoardSize();
		Board board = new Board(boardsize);
		InstantiateBoard(board);
		return board;
	}
	
    [Obsolete("Old map generation, may be removed")]
	private IntCouple GenerateBoardSize() {
		int x = (int) Random.Range(minX, maxX);
		int y = (int) Random.Range(minY, maxY);
		
		Board board = new Board(x,y);
		return new IntCouple(x, y);
	}
	
	/*public void InstantiateBoard(Board board) {
		boardHolder = new GameObject("Board").transform;
		board.BoardHolder = boardHolder;
		//int xOffset = 0;

		// On peut perdre une case de longueur, on s'en fout pour le moment
		int boundX = board.SizeX / 2;
		for (int x = -boundX; x < boundX ; x++) {
			for (int y = 0; y < board.SizeY ; y++) {
				GameObject bgToInstantiate = backgroundTiles[Random.Range(0, backgroundTiles.Length)];
				GameObject bgInstance = Instantiate(bgToInstantiate, new Vector3(x,y,1.0f), Quaternion.identity) as GameObject;
				bgInstance.transform.SetParent(boardHolder);

				GameObject toInstantiate;
				if (x == -boundX && y == 0)
				{
					toInstantiate = cornerbgTiles[Random.Range(0, cornerbgTiles.Length)];
				} else if (x == boundX-1 && y == 0)
				{
					toInstantiate = cornerbdTiles[Random.Range(0, cornerbdTiles.Length)];
				} else if (x == -boundX && y == board.SizeY-1)
				{
					toInstantiate = cornerhgTiles[Random.Range(0, cornerhgTiles.Length)];
				} else if (x == boundX-1 && y == board.SizeY-1)
				{
					toInstantiate = cornerhdTiles[Random.Range(0, cornerhdTiles.Length)];
				} else if (x == -boundX)
				{
					toInstantiate = leftTiles[Random.Range(0, leftTiles.Length)];
				} else if (x == boundX-1)
				{
					toInstantiate = rightTiles[Random.Range(0, rightTiles.Length)];
				} else if (y == 0)
				{
					toInstantiate = bottomTiles[Random.Range(0, bottomTiles.Length)];
				} else if (y == board.SizeY-1)
				{
					toInstantiate = topTiles[Random.Range(0, topTiles.Length)];
				} else {
					toInstantiate = null;
				}
				if (toInstantiate != null)
				{
					GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder); 
				}
			}
		}
	}*/

	public void InstantiateBoard(Board board) 
	{
		boardHolder = new GameObject("Board").transform;
		board.BoardHolder = boardHolder;

        // TODO: remove offsets when all enemies position are taken from xml
        int xOffset = -5;
        int yOffset = -95;
		GameObject toInstantiate = null;

		foreach (ArrayList layer in board.Layers) 
		{
			for (int y = 0; y < board.SizeY; y++) 
			{
				ArrayList currentRow = layer.ToArray()[y] as ArrayList;
				for (int x = 0; x < board.SizeX; x++)
				{
					toInstantiate = null;
					string elem = currentRow.ToArray()[x] as string;
					switch(elem) 
					{
					case "0":
						break;
					case "1":
						toInstantiate = backgroundTiles[Random.Range(0, backgroundTiles.Length)];
						break;
					case "2":
						toInstantiate = cliffhgTiles[Random.Range(0, cliffhgTiles.Length)];
						break;
					case "3":
						toInstantiate = cliffbgTiles[Random.Range(0, cliffbgTiles.Length)];
						break;
					case "4":
						toInstantiate = fullTiles[Random.Range(0, fullTiles.Length)];
						break;
					case "5":
						toInstantiate = cornerhgTiles[Random.Range(0, cornerhgTiles.Length)];
						break;
					case "6":
						toInstantiate = topTiles[Random.Range(0, topTiles.Length)];
						break;
					case "7":
						toInstantiate = cornerhdTiles[Random.Range(0, cornerhdTiles.Length)];
						break;
					case "8":
                        Debug.Log("Case 8 found!");
						break;
					case "9":
						toInstantiate = leftTiles[Random.Range(0, leftTiles.Length)];
						break;
					case "10":
                        _initPlayerPosition = new Vector3(x + xOffset, board.SizeY - y + yOffset, 0f);
						break;
					case "11":
						toInstantiate = rightTiles[Random.Range(0, rightTiles.Length)];
						break;
					case "12":
						toInstantiate = cliffhdTiles[Random.Range(0, cliffhdTiles.Length)];
						break;
					case "13":
						toInstantiate = cornerbgTiles[Random.Range(0, cornerbgTiles.Length)];
						break;
					case "14":
						toInstantiate = bottomTiles[Random.Range(0, bottomTiles.Length)];
						break;
					case "15":
						toInstantiate = cornerbdTiles[Random.Range(0, cornerbdTiles.Length)];
						break;
					case "16":
						toInstantiate = cliffbdTiles[Random.Range(0, cliffbdTiles.Length)];
						break;
					default:
						toInstantiate = backgroundTiles[Random.Range(0, backgroundTiles.Length)];
						break;
					}
					if (toInstantiate != null) 
					{
						GameObject instance = Instantiate(toInstantiate, new Vector3(x+xOffset,board.SizeY-y+yOffset,0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent(boardHolder); 
					}
				}
			}
		}
	}
}
