using UnityEngine;
using System.Collections;

public class DropManager : MonoBehaviour {

    public GameObject coin;

    private DropManager instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CreateCoin(Vector3 pos)
    {
        Instantiate(coin, pos, Quaternion.identity);
    }
}
