using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public Text healthText;
    public Text strengthText;
    public Text speedText;
    public Text jumpText;

    public Player player;

	// Use this for initialization
	void Start ()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthText.text = player.Health.ToString();
        speedText.text = player.Speed.ToString();
        strengthText.text = player.Strength.ToString();
        jumpText.text = player.JumpPower.ToString();
        //UpdateAliceRendering();
	}

    /*private void UpdateAliceRendering()
    {
        GetComponentInChildren<AliceRenderer>().UpdateAliceRendering();
    }*/
}
