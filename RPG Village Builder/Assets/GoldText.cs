using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Helpers;

public class GoldText : MonoBehaviour {

    private EconomyManager _economyManager;
    private Text _text;

	// Use this for initialization
	void Start () {
        _economyManager = FindObjectOfType<EconomyManager>();
        _text = GetComponent<Text>();

        ObjectChecker.CheckNullity(_economyManager, "Economy manager not found");
        ObjectChecker.CheckNullity(_text, "Component Text not found");
	}
	
	// Update is called once per frame
	void Update () {
        _text.text = _economyManager.gold.ToString();
	}
}
