using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientWidget : MonoBehaviour {

    public Config config;
    public Text noConfigText;
    public Text nameText;
    public Text typeText;
    public Text ipPortText;
    public Button connectButton;
    public Button disconnectButton;
    public Client client;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateButtons();
        if (config == null || string.IsNullOrEmpty(config.ip) || string.IsNullOrEmpty(config.port))
        {
            noConfigText.enabled = true;
            noConfigText.gameObject.transform.localScale = Vector3.one;
            nameText.text = "";
            typeText.text = "";
            ipPortText.text = "";
        }
        else
        {
            noConfigText.enabled = false;
            noConfigText.gameObject.transform.localScale = Vector3.zero;
            nameText.text = config.name;
            typeText.text = config.type == Config.ConfigType.Client ? "Client" : "Serveur";
            ipPortText.text = config.ip + ":" + config.port;
        }
    }

    private void UpdateButtons()
    {
        if (client != null && config != null && config.name != "")
        {
            connectButton.enabled = true;
        }
        else
        {
            connectButton.enabled = false;
        }
        if (client != null)
        {
            disconnectButton.enabled = true;
        }
        else
        {
            disconnectButton.enabled = false;
        }
    }
}
