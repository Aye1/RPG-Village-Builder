using UnityEngine;
using System.Collections;

public class LivingCollectible : MonoBehaviour {

    public float scalingSpeed = 0.005f;
    public float maxScale = 1.2f;
    public float minScale = 0.8f;

    public float initialScale;

	// Use this for initialization
	void Start () {
        initialScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale += new Vector3(scalingSpeed, scalingSpeed, 0.0f);
        if (transform.localScale.x >= maxScale - initialScale || transform.localScale.x <= minScale - initialScale)
        {
            scalingSpeed *= -1;
        }
	}
}
