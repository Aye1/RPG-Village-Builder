using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public Building house;
    public Building[] referenceBuildings;

    public int currentBuildingIndex = 0;

    private Dictionary<Vector3, Building> _buildings;

    private const float yOffset = 0.01f;

    // Use this for initialization
    void Start () {
        _buildings = new Dictionary<Vector3, Building>();
	}
	
	// Update is called once per frame
	void Update () {
        ManageKeys();
	}

    private void ManageKeys()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeBuildingTool();
        }
    }

    public bool IsPosOccupied(Vector3 pos)
    {
        return _buildings.ContainsKey(pos);
    }

    public void CreateBuilding(Vector3 pos)
    {
        if (CanBuildAtPos(pos))
        {
            Building referenceBuilding = referenceBuildings[currentBuildingIndex];
            Building buildingCopy = Instantiate(referenceBuilding, transform);
            buildingCopy.transform.position = new Vector3 (pos.x, yOffset, pos.z);
            _buildings.Add(pos, buildingCopy);
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

    public void ChangeBuildingTool()
    {
        if (referenceBuildings.Length > 1)
        {
            currentBuildingIndex = (currentBuildingIndex + 1) % referenceBuildings.Length;
        }
    }

    public Building GetSelectedBuilding()
    {
        return referenceBuildings[currentBuildingIndex];
    }
}
