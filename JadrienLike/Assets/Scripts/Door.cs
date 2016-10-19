using UnityEngine;

public class Door : MonoBehaviour {

    public string destination;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //other.GetComponentInParent<Player>().Teleport(Destination);
                other.GetComponentInParent<Player>().OnEnterDoor(this);
            }
        }
    }
}
