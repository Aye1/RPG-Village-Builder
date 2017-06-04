using UnityEngine;

public static class UIHelper
{
    public static void HideBehaviour(MonoBehaviour obj)
    {
        obj.enabled = false;
        obj.gameObject.transform.localScale = Vector3.zero;
    }

    public static void DisplayBehaviour(MonoBehaviour obj)
    {
        obj.enabled = true;
        obj.gameObject.transform.localScale = Vector3.one;
    }
}

