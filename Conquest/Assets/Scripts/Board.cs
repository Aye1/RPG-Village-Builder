using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Board : MonoBehaviour {

    public List<Case> cases;
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

    public bool IsMoveAllowed(Vector2 pos)
    {
        bool res = true;
        Case c = GetCaseAtPos(pos);
        res = res && c.status != Case.CaseStatus.Totem;
        return res;
    }

    public void PlayMove(Vector2 pos, GameManager.Player player)
    {
        if (!IsMoveAllowed(pos))
        {
            return;
        }

        foreach(Case c in cases)
        {
            if (c.status != Case.CaseStatus.Totem)
            {
                if ((c.x == pos.x) ^ (c.y == pos.y))
                {
                    ReturnCase(c, player);
                }
                if ((c.x == pos.x) && (c.y == pos.y))
                {
                    c.status = Case.CaseStatus.Totem;
                }
            }
        }
    }

    public void PlayMoveWithAnimation(Vector2 pos, GameManager.Player player)
    {
        if (IsMoveAllowed(pos))
        {
            PutTotemOnPlayedCase(pos);

            Lookup<int, Case> casesToReturn = GetCasesToReturnWithDistance(pos);

            foreach (IGrouping<int, Case> group in casesToReturn)
            {
                foreach (Case c in group)
                {
                    /*yield return new WaitForSeconds(1.0f);
                    ReturnCase(c, player);*/
                    StartCoroutine(ReturnCasesAtDistance(casesToReturn, 1, player));
                }

            }
        }
    }

    private IEnumerator ReturnCasesAtDistance(Lookup<int, Case> lookup, int dist, GameManager.Player player)
    {
        Debug.Log("Start Coroutine with dist " + dist.ToString());
        if (lookup.Contains(dist))
        {
            IEnumerable<Case> group = lookup[dist];
            Debug.Log("Turning cases at dist " + dist.ToString());
            foreach (Case c in group)
            {
                Debug.Log("Turning case " + c.x + " " + c.y);
                ReturnCase(c, player);
            }
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ReturnCasesAtDistance(lookup, dist + 1, player));
        }
    }

    /*private IEnumerator WaitReturnAnimation(Case c, GameManager.Player player)
    {
        yield return new WaitForSeconds(1.0f);
        ReturnCase(c, player);
    }*/

    /// <summary>
    /// Returns the list of cases to return when a totem is played
    /// </summary>
    /// <param name="pos">Position where the totem is played</param>
    /// <returns></returns>
    private List<Case> GetCasesToReturn(Vector2 pos)
    {
        List<Case> res = new List<Case>();
        foreach (Case c in cases)
        {
            if ((c.x == pos.x) ^ (c.y == pos.y) && c.status != Case.CaseStatus.Totem)
            {
                res.Add(c);
            }
        }
        return res;
    }

    /// <summary>
    /// Returns a lookup with all cases to return, with their distance to the totem as a key
    /// </summary>
    /// <param name="pos">Position where the totem is played</param>
    /// <returns></returns>
    private Lookup<int, Case> GetCasesToReturnWithDistance(Vector2 pos)
    {
        Lookup<int, Case> res = (Lookup<int, Case>)GetCasesToReturn(pos).ToLookup(o => DistanceBetweenPoints(new Vector2(o.x, o.y), pos), o => o);
        return res;
    }

    private int DistanceBetweenPoints(Vector2 pos1, Vector2 pos2)
    {
        int dist = 0;
        dist += (int)Mathf.Abs(pos1.x - pos2.x);
        dist += (int)Mathf.Abs(pos1.y - pos2.y);
        return dist;
    }

    private void PutTotemOnPlayedCase(Vector2 pos)
    {
        Case c = GetCaseAtPos(pos);
        c.status = Case.CaseStatus.Totem;
    }

    private void ReturnCase(Case c, GameManager.Player player)
    {
        c.status = player == GameManager.Player.P1 ? Case.CaseStatus.P1 : Case.CaseStatus.P2;
    }

    private void InstantiateBoard()
    {
        cases = new List<Case>();
        for(int i=0; i<width; i++)
        {
            for(int j=0; j<height; j++)
            {
                Case currentCase = Instantiate(basicCase, new Vector3(i+0.5f, j+0.5f, 0.0f), Quaternion.identity);
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

    public Case GetCaseAtPos(Vector2 pos)
    {
        foreach (Case c in cases)
        {
            if (c.x == pos.x && c.y == pos.y)
            {
                return c;
            }
        }
        return null;
    }
}
