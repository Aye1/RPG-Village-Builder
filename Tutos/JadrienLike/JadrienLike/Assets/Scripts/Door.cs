using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private Vector3 _destination;

    public Vector3 Destination
    {
        get
        {
            return _destination;
        }

        set
        {
            if (value != null)
            {
                _destination = value;
            }
        }
    }

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
