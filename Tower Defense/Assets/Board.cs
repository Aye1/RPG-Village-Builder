using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isPositionValid(Vector3 pos)
    {
        bool res = true;
        Color pixelColor = GetComponentInChildren<SpriteRenderer>().sprite.texture.GetPixel((int)pos.x, (int)pos.y);
        if (pixelColor == Color.white)
        {
            res = false;
        }
        return res;
    }
}
