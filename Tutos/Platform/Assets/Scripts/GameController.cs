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

		currentBoard = boardManager.CreateBoard();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
