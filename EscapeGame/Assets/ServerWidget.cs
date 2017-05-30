using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerWidget : MonoBehaviour {

    public Config config;
    public Text noConfigText;
    public Text nameText;
    public Text typeText;
    public Text ipPortText;
    public Button launchButton;
    public Button stopButton;
    public Server server;
    //public Client client;

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
        } else
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
        if (server != null && config != null && config.name != ""
            && server.State == Server.ServerState.Stopped)
        {
            launchButton.enabled = true;
        }
        else
        {
            launchButton.enabled = false;
        }
        if (server != null && server.State == Server.ServerState.Launched)
        {
            stopButton.enabled = true;
        }
        else
        {
            stopButton.enabled = false;
        }
    }
}
