using UnityEngine;
using System.Collections;

public class Chair : Destructible {

    private DropManager dropManager;

	// Use this for initialization
	void Start () {
        dropManager = FindObjectOfType<DropManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnHit()
    {
        dropManager.CreateCoin(gameObject.transform.position);
        Destroy(gameObject);
    }
}
