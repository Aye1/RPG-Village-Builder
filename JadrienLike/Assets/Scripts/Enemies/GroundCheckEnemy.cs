using UnityEngine;
using System.Collections;

public class GroundCheckEnemy : MonoBehaviour {
    private Enemy enemy;

    // Use this for initialization
    void Start()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colliderObject = col.GetComponent<Collider2D>().gameObject;
        //TODO simplifier en utilisant uniquement les layers
        if (colliderObject.CompareTag("Corner")
            || colliderObject.layer == LayerMask.NameToLayer("Wall")
            || colliderObject.CompareTag("Enemy"))
        {
            enemy.groundAhead = false;
        }
      
        else if (colliderObject.CompareTag("Player"))
        {
        }
        else if (colliderObject.layer == LayerMask.NameToLayer("Floor"))
        {
            enemy.groundAhead = true;
        }
    }

  /*  void OnTriggerStay2D(Collider2D col)
    {

        if (col.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            enemy.groundAhead = true;
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        enemy.groundAhead = false;
    }*/
}