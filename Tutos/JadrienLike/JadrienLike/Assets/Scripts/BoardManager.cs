using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
	
	public GameObject[] floorTiles;
	public GameObject[] platformTiles;
	
	private int minX = 10;
	private int maxX = 30;
	private int minY = 1;
	private int maxY = 1;
	private int offsetX = 4;
	
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
		int yglobal = 0;
		int xOffset = 0;
		for (int x = 0; x < board.SizeX ; x++) {
			xOffset = x*offsetX;
            Debug.Log("xoffset " +xOffset);
            Debug.Log("offsetx " + offsetX);
			for (int y = 0; y < board.SizeY ; y++) {
			//	yglobal--;
				GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
				GameObject instance = Instantiate(toInstantiate, new Vector3(xOffset,yglobal,0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder); 
			}
		}
	}
}
