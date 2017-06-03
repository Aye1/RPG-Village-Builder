using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

    public enum ServerState { Stopped, Launching, Launched }

    private ServerState serverState;
    private Config config;
    public ServerLogs logs;

    public ServerState State
    {
        get { return serverState; }
    }

    public Config ServerConfig
    {
        get { return config; }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LaunchServer(ServerWidget configWidget)
    {
        if (serverState != ServerState.Stopped)
        {
            Debug.LogError("Server not stopped, launch interrupted");
            return;
        }
        if (configWidget != null)
        {
            serverState = ServerState.Launching;
            config = configWidget.config;
            int port = 0 ;
            int.TryParse(config.port, out port);
            if (port != 0)
            {
                NetworkServer.Listen(port);
                NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
                serverState = ServerState.Launched;
                Debug.Log("Server launched on port " + config.port);
            } else
            {
                Debug.LogError("Error while launching the server : invalid port");
            }
        } 
    }

    public void StopServer()
    {
        if (serverState != ServerState.Launched)
        {
            Debug.LogError("Server not fully launched, can't stop it at the moment. Stop cancelled.");
            return;
        }
        NetworkServer.DisconnectAll();
        serverState = ServerState.Stopped;
    }

    private void OnClientConnected(NetworkMessage msg)
    {
        Debug.Log("New client connected");
        logs.logs.Add("New client connected");
    }

    public void SimulateClientConnected()
    {
        OnClientConnected(new NetworkMessage());
    }
}
