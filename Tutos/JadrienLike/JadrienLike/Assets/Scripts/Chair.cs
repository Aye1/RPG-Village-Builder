using UnityEngine;
using System.Collections;

public class Chair : Destructible {

    private DropManager dropManager;
    public LivingCollectible Drop;
	// Use this for initialization
	void Start () {
        dropManager = FindObjectOfType<DropManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnHit()
    {
        dropManager.CreateSphere(Drop, gameObject.transform.position);
        Destroy(gameObject);
    }
}
