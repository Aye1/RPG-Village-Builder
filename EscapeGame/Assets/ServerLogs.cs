using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLogs : MonoBehaviour {

    public List<string> logs;
    private int currentLogsCount;

	// Use this for initialization
	void Start () {
        currentLogsCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (logs.Count != currentLogsCount)
        {
            int nbMissingLogs = logs.Count - currentLogsCount;
            string lastLog = logs.ToArray()[logs.Count-1];
        }
	}
}
