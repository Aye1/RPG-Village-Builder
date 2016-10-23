using UnityEngine;
using UnityEngine.UI;
public class AliceRenderer : MonoBehaviour {
    
    private Player _player;

	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        //_aliceRenderer.transform.position = new Vector3(10.0f, 10.0f, 0.0f);
    }

    void OnEnable()
    {
        UpdateAliceRendering();
    }

    public void UpdateAliceRendering()
    {
        if (_player != null)
        {
            Image[] childrens = gameObject.GetComponentsInChildren<Image>();
            foreach ( Image child in childrens)
            {
                if(child.name == "Hat")
                {
                    child.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag(TagConstants.TagHat).GetComponent<SpriteRenderer>().sprite;
                }
            }
        }
    }
}
