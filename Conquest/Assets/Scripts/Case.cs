using UnityEngine;
using System.Collections;

public class Case : MonoBehaviour {

    public enum CaseStatus { P1, P2, Totem, Empty };

    public int value;
    public CaseStatus status;
    public int x;
    public int y;

    public Color colorP1;
    public Color colorP2;
    public Color colorEmpty = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color colorTotem = new Color(0.0f, 0.0f, 0.0f, 0.5f);

	// Use this for initialization
	void Start () {
        value = 0;
        status = CaseStatus.Empty;
	}
	
	// Update is called once per frame
	void Update () {
	    if (status == CaseStatus.P1)
        {
            SetColor(colorP1);
        }
        if (status == CaseStatus.P2)
        {
            SetColor(colorP2);
        }
        if (status == CaseStatus.Empty)
        {
            SetColor(colorEmpty);
        }
        if (status == CaseStatus.Totem)
        {
            SetColor(colorTotem);
        }
    }

    private void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
