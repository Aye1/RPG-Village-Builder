using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {

    private Player _player;

	// Use this for initialization
	void Start () {
        _player = gameObject.GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagConstants.TagEnemy))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            _player.Attack(enemy, _player.Strength);
        }
    }
}
