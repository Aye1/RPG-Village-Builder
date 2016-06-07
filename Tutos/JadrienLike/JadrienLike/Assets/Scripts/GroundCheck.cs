using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{

    private Player player;

    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            player.grounded = true;
        }
       else if (col.GetComponent<Collider2D>().CompareTag("Enemy_top"))
        {
            player.Attack(col.GetComponent<Enemy>(), player.FootHit);
        }
        else
        { 
            player.grounded = false;
        };
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            player.grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        player.grounded = false;
    }
}
