using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;
    public float bulletSpeed;
    public void Update()
    {
        CheckPosition();
    }

    void CheckPosition()
    {

        if (Mathf.Abs(this.transform.position.x % RoomManager.RoomWidth) <= 0.3f || Mathf.Abs(this.transform.position.y % RoomManager.RoomHeight) <= 0.2f)
        {
            Destroy(this.gameObject);
        }
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

    public void Scale(float factorScale)
    {
        gameObject.GetComponent<Transform>().localScale = new Vector3(factorScale, factorScale, 1);
       
    }

    public void SetColor(Color colorBullet)
    {
        colorBullet.a = 1;
        gameObject.GetComponent<SpriteRenderer>().color = colorBullet;
        
    }
}
