using UnityEngine;
using System.Collections;

public class ProgressBar : ScriptableObject {


    public string displayName;
    Vector2 size;

    public int minValue;
    public int maxValue;
    int currentValue;


    Texture2D barTexture;
    Texture2D emptyTexture;

    Rect barRect;
	
    public ProgressBar (Vector2 size, int minValue, int maxValue, string displayName, Color emptyColor, Color barColor, Color textColor)
    {
        this.size = size;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.displayName = displayName;
        this.barRect = new Rect(0, 0, size.x, size.y);


    }
}
