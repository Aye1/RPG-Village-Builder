using UnityEngine;
using System.Collections;

public abstract class Equipment : Item {
    protected string _tag;

    public void AttachToPlayer()
    {
        GameObject currentHat = GameObject.FindGameObjectsWithTag(_tag)[0];

        this.transform.parent = currentHat.transform.parent;
        this.transform.localScale = currentHat.transform.localScale;
        this.transform.position = currentHat.transform.position;
        gameObject.tag = _tag;
        this.GetComponent<BoxCollider2D>().enabled = false;
        currentHat.GetComponent<Equipment>().DetachFromPlayer();
    }

    public void DetachFromPlayer()
    {
        this.transform.parent = null;
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        this.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.tag = "Item";
        GetComponent<Renderer>().enabled = true;
    }

    public override void OnPlayerTouches()
    {
        AttachToPlayer();
    }
}
