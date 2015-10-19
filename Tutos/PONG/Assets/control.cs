using UnityEngine;
using System.Collections;

public class control : MonoBehaviour {

    public Transform Player1;
    public Transform Player2;
    public Camera MainCam;
    public BoxCollider2D North;
    public BoxCollider2D South;
    public BoxCollider2D West;
    public BoxCollider2D East;
    // Update is called once per frame
    void Start () {
        North.size = new Vector2(MainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x ,1f);
        North.offset = new Vector2(0f, MainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);

        South.size = new Vector2(MainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        South.offset = new Vector2(0f, MainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y - 0.5f);

        West.size = new Vector2( 1f,MainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        West.offset = new Vector2( MainCam.ScreenToWorldPoint(new Vector3( Screen.width, 0f, 0f)).x + 0.5f,0f);

        East.size = new Vector2(1f,MainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
        East.offset = new Vector2( MainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f,0f);

        Player1.position= new Vector3(MainCam.ScreenToWorldPoint(new Vector3(75f, 0f, 0f)).x, Player1.position.y, 0f);
        Player2.position = new Vector3(MainCam.ScreenToWorldPoint(new Vector3(Screen.width - 75f, 0f, 0f)).x, Player2.position.y, 0f);

    }
}
