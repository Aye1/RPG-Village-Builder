using UnityEngine;
using System.Collections;

public class Bullet_Collision : MonoBehaviour {

    public int damage;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Enemy"))
        {
           
            gameObject.SetActive(false);
            if (col.gameObject.CompareTag("Player"))
            {
                col.GetComponent<Player>().Damage(damage);
                Debug.Log("coucou");
            }
        }
    }
}
