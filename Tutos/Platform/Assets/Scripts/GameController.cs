using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject player;
	public BoardManager boardManager;

	// Use this for initialization
	void Start () {
		Board board = boardManager.CreateBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
