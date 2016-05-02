using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
	
	//public GameObject[] floorTiles;
	//public GameObject[] undergroundTiles;

	public GameObject[] topTiles;
	public GameObject[] bottomTiles;
	public GameObject[] leftTiles;
	public GameObject[] rightTiles;
	public GameObject[] cornerbgTiles;
	public GameObject[] cornerbdTiles;
	public GameObject[] cornerhgTiles;
	public GameObject[] cornerhdTiles;
	public GameObject[] cliffbgTiles;
	public GameObject[] cliffbdTiles;
	public GameObject[] cliffhgTiles;
	public GameObject[] cliffhdTiles;
	public GameObject[] backgroundTiles;
	
	private int minX = 20;
	private int maxX = 40;
	private int minY = 20;
	private int maxY = 40;
	//private int offsetX = 1;
	private int undergroundDepth = -10;
	
	private Transform boardHolder;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public Board CreateBoard() {
		IntCouple boardsize = GenerateBoardSize();
		Board board = new Board(boardsize);
		InstantiateBoard(board);
		return board;
	}
	
	private IntCouple GenerateBoardSize() {
		int x = (int) Random.Range(minX, maxX);
		int y = (int) Random.Range(minY, maxY);
		
		Board board = new Board(x,y);
		return new IntCouple(x, y);
	}
	
	private void InstantiateBoard(Board board) {
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
	}
}
