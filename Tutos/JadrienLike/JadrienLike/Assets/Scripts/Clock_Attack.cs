using UnityEngine;
using System.Collections;

public class Clock_Attack : MonoBehaviour {

    private Clock clock;

	void Awake()
    {
        clock = gameObject.GetComponentInParent<Clock>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            clock.Attack(); 
        }
    }
}
