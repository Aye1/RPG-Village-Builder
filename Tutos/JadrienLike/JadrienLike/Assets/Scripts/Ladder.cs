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
        if(other.CompareTag("Player"))
        {
            Debug.Log ("Player on ladder");
            Rigidbody2D body = GetPlayerBodyFromCollider(other);
            body.isKinematic = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            Rigidbody2D body = GetPlayerBodyFromCollider(other);
            body.isKinematic = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
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
        }
    }

    private Player GetPlayerFromCollider(Collider2D other)
    {
        return other.GetComponentInParent<Player>();
    }

    private Rigidbody2D GetPlayerBodyFromCollider(Collider2D other)
    {
        Player player = GetPlayerFromCollider(other);
        return player.GetComponentInChildren<Rigidbody2D>();
    }
}
