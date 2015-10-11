using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject player;
	public BoardManager boardManager;

	private Board currentBoard = null;

	// Use this for initialization
	void Start () {
		currentBoard = boardManager.CreateBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
