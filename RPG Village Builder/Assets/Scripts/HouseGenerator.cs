using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour {

    public int nbX;
    public int nbY;

    public int previousX;
    public int previousY;

    public GameObject house;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (nbX != previousX || nbY != previousY)
        {
            CreateHouses();
        }
	}

    private void CreateHouses()
    {
        const int space = 20;
        for (int i = 0; i < nbX; i++)
        {
            for (int j = 0; j < nbY; j++)
            {
                GameObject newHouse = Instantiate(house, transform);
                newHouse.transform.position = new Vector3(i * space, 0, j * space);
            }
        }
        previousX = nbX;
        previousY = nbY;
    }

}
