using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour {

    private Config config;
    private NetworkClient client;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ConnectClient(ClientWidget configWidget)
    {
        config = configWidget.config;
        client = new NetworkClient();
        int port = 0;
        int.TryParse(config.port, out port);
        client.Connect(config.ip, port);
        client.RegisterHandler(MsgType.Connect, OnConnected);
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
