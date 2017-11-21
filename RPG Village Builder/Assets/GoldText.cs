using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour {

    private EconomyManager _economyManager;
    private Text _text;

	// Use this for initialization
	void Start () {
        _economyManager = FindObjectOfType<EconomyManager>();
        if (_economyManager == null)
        {
            Debug.LogError("Economy Manager not found");
        }
        _text = GetComponent<Text>();
        if (_text == null)
        {
            Debug.LogError("Component Text not found");
        }
	}
	
	// Update is called once per frame
	void Update () {
        _text.text = _economyManager.gold.ToString();
	}
}
