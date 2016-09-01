using UnityEngine;
using System.Collections;

public class DropManager : MonoBehaviour {

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

    public void CreateSphere(LivingCollectible Drop, Vector3 pos)
    {
        Instantiate(Drop, pos, Quaternion.identity);
    }
}
