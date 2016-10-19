using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ladder_Detection"))
        {
            GetPlayerFromCollider(other).OnEnterLadder(this);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Ladder_Detection"))
        {
            GetPlayerFromCollider(other).OnExitLadder();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Ladder_Detection"))
        {
            Player player = this.GetPlayerFromCollider(other);
            if(Input.GetKey(KeyCode.UpArrow))
            {
                player.MoveUp();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                player.MoveDown();
            }
            else
            {
                player.GetComponent<Animator>().SetBool("Ladder_Climb", false);
            }
            player.OnStayLadder();
        }
    }

    private Player GetPlayerFromCollider(Collider2D other)
    {
        return other.GetComponentInParent<Player>();
    }
}
