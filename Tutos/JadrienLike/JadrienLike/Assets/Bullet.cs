using UnityEngine;
using System.Collections;

public class Bullet_collision : MonoBehaviour {

    public int dammage;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
        else if (col.gameObject.CompareTag("Player"))
            {
            col.GetComponent<Player>().Damage(dammage, transform.position);
        }
    }

}
