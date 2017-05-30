using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public enum Player { P1, P2, undefined };
    public Player currentPlayer;
    public int turn;
    public bool isGameFinished;
    public Player winner;
    public int scoreP1;
    public int scoreP2;
    public Board board;

    public Text scoreP1Text;
    public Text scoreP2Text;
    public Text gameEndText;
    public Button resetButton;

	// Use this for initialization
	void Start () {
        ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
        ManageClick();
        UpdateUI();
	}

    public void ResetGame()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        winner = Player.undefined;
        turn = 0;
        currentPlayer = Player.P1;
        isGameFinished = false;
        board.ResetBoard();
    }

    private void ManageClick()
    {
        if(Input.GetMouseButtonDown(0) && !isGameFinished)
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Mouse click at " + currentPos);
            Vector2 boardPos = new Vector2(Mathf.Floor(currentPos.x), Mathf.Floor(currentPos.y));
            board.PlayMove(boardPos, currentPlayer);
            ChangeCurrentPlayer();
            CountScores();
        }
    }
    
    private void UpdateUI()
    {
        scoreP1Text.text = scoreP1.ToString();
        scoreP2Text.text = scoreP2.ToString();
        gameEndText.enabled = isGameFinished;
    }

    private void ChangeCurrentPlayer()
    {
        currentPlayer = currentPlayer == Player.P1 ? Player.P2 : Player.P1;
    }

    private void CountScores()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        int emptyCases = 0;
        foreach (Case c in board.cases)
        {
            switch (c.status)
            {
                case Case.CaseStatus.Empty:
                    emptyCases++;
                    break;
                case Case.CaseStatus.P1:
                    scoreP1++;
                    break;
                case Case.CaseStatus.P2:
                    scoreP2++;
                    break;
                default:
                    break;
            }
        }
        if (emptyCases == 0)
        {
            isGameFinished = true;
        }
    }
}
