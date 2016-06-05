using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;
    public float bulletSpeed;
    private Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
    
        if (col.gameObject.CompareTag("Physical_Background") || col.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<Player>().Damage(damage, transform.position);
          
            Destroy(gameObject);
        }

    }
}
