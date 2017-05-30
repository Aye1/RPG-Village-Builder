using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    public ArrayList cases;
    public Case basicCase;
    public const int width = 10;
    public const int height = 10;

	// Use this for initialization
	void Start () {
        InstantiateBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayMove(Vector2 pos, GameManager.Player player)
    {
        foreach(Case c in cases)
        {
            if (c.status != Case.CaseStatus.Totem)
            {
                if ((c.x == pos.x) ^ (c.y == pos.y))
                {
                    c.status = player == GameManager.Player.P1 ? Case.CaseStatus.P1 : Case.CaseStatus.P2;
                }
                if ((c.x == pos.x) && (c.y == pos.y))
                {
                    c.status = Case.CaseStatus.Totem;
                }
            }
        }
    }

    private void InstantiateBoard()
    {
        cases = new ArrayList();
        for(int i=0; i<width; i++)
        {
            for(int j=0; j<height; j++)
            {
                Case currentCase = (Case) Instantiate(basicCase, new Vector3(i+0.5f, j+0.5f, 0.0f), Quaternion.identity);
                currentCase.x = i;
                currentCase.y = j;
                currentCase.transform.parent = transform;
                cases.Add(currentCase);
            }
        }
    }

    public void ResetBoard()
    {
        foreach(Case c in cases)
        {
            c.status = Case.CaseStatus.Empty;
        }
    }
}
