using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public Building house;
    private Dictionary<Vector3, Building> _buildings;

    private const float yOffset = 0.01f;

    // Use this for initialization
    void Start () {
        _buildings = new Dictionary<Vector3, Building>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsPosOccupied(Vector3 pos)
    {
        return _buildings.ContainsKey(pos);
    }

    public void CreateBuilding(Vector3 pos)
    {
        if (CanBuildAtPos(pos))
        {
            Building houseCopy = Instantiate(house, transform);
            houseCopy.transform.position = new Vector3 (pos.x, yOffset, pos.z);
            _buildings.Add(pos, houseCopy);
        }
    }

    public bool CanBuildAtPos(Vector3 pos)
    {
        return !IsPosOccupied(pos) && !IsYTooHigh(pos);
    }

    public bool IsYTooHigh(Vector3 pos)
    {
        return pos.y > 0.1f;
    }
}
