using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;
    public float bulletSpeed;

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

    public void Scale(float factorScale)
    {
        gameObject.GetComponent<Transform>().localScale = new Vector3(factorScale, factorScale, 1);
    }
}
