using UnityEngine;
using System.Collections;

public class Bullet_Collision : MonoBehaviour {

    public int damage;
    private Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
    
        if (col.gameObject.CompareTag("Physical_Background"))
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<Player>().Damage(damage);
            StartCoroutine(player.Knockback(1f, 500, player.transform.position));
            Destroy(gameObject);
        }

    }
}
