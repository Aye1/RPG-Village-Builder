using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLogs : MonoBehaviour {

    public List<string> logs;
    private int currentLogsCount;
    public LayoutGroup layout;
    public Text referenceText;
    private ScrollRect scroll;

    private bool shouldScrollToBottom;

	// Use this for initialization
	void Start () {
        currentLogsCount = 0;
        layout = gameObject.GetComponentInChildren<VerticalLayoutGroup>();
        referenceText.gameObject.transform.localScale = Vector3.zero;
        scroll = gameObject.GetComponentInChildren<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
        CreateNewLogsText();
	}

    private void CreateNewLogsText()
    {
        if (shouldScrollToBottom)
            SetPositionToBottom();
        if (logs.Count != currentLogsCount)
        {
            int nbMissingLogs = logs.Count - currentLogsCount;
            string lastLog = logs.ToArray()[logs.Count - 1];
            CreateTextWithLog(lastLog);
            currentLogsCount += nbMissingLogs;
            shouldScrollToBottom = true;
            //SetPositionToBottom();
        }
    }

    private void CreateTextWithLog(string log)
    {
        Text newText = Instantiate(referenceText);
        newText.gameObject.transform.localScale = Vector3.one;
        newText.text = log;
        newText.gameObject.transform.SetParent(layout.transform);
    }

    private void SetPositionToBottom()
    {
        if (scroll != null && shouldScrollToBottom)
        {
            scroll.verticalNormalizedPosition = 0.0f;
            shouldScrollToBottom = false;
        }
    }
}
