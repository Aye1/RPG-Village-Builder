using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public GameObject[] floorTiles;
	public GameObject[] platformTiles;

	private int minX = 10;
	private int maxX = 30;
	private int minY = 5;
	private int maxY = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Board CreateBoard() {
		IntCouple boardsize = GenerateBoardSize();
		return new Board(boardsize);
	}

	private IntCouple GenerateBoardSize() {
		int x = (int) Random.Range(minX, maxX);
		int y = (int) Random.Range(minY, maxY);

		Board board = new Board(x,y);
		return new IntCouple(x, y);
	}
}
