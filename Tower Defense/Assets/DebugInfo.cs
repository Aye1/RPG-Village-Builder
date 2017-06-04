using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour {

    public Text posText;
    public Text worldPosText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Input.mousePosition;
        posText.text = "<X : " + mousePos.x + " - Y : " + mousePos.y + ">";
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosText.text = "<X : " + worldPos.y + " - Y : " + worldPos.y + ">";
	}
}
