using UnityEngine;
using System.Collections;

public class Clock_Attack : MonoBehaviour {

    private Enemy enemy;

	void Awake()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("Attack can't find its parent Enemy");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && enemy != null)
        {
            enemy.Attack(); 
        }
    }
}
