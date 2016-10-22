using UnityEngine;
using System.Collections;

public class BlueRibbon : Item {

	// Use this for initialization
	public override void Init () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void AttachToPlayer()
    {
        GameObject currentHat = GameObject.FindGameObjectsWithTag("hat")[0];

        this.transform.parent = currentHat.transform.parent;
        this.transform.localScale = currentHat.transform.localScale;
        this.transform.position = currentHat.transform.position;

        gameObject.tag = "hat";
        currentHat.tag = "Item";

        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void OnPlayerTouches()
    {
        AttachToPlayer();
    }
}
