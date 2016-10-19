using UnityEngine;
using System.Collections;

public class ScalingCollectible : MonoBehaviour
{
    public float multiplicatorFactor = 1.0f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            Vector3 scale = player.transform.localScale;
            player.transform.localScale = new Vector3(scale.x * multiplicatorFactor, scale.y * multiplicatorFactor, scale.z);
            gameObject.SetActive(false);
        }
    }
}
