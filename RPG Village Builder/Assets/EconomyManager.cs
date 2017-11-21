using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour {

    public int gold = 8000;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool CanSpendGold(int amount)
    {
        return (gold - amount) >= 0;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void SpendGold(int amount)
    {
        if (CanSpendGold(amount))
        {
            gold -= amount;
        }
    }
}
