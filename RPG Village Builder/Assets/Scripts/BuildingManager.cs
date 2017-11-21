using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public Building[] referenceBuildings;
    public int currentBuildingIndex = 0;

    private Dictionary<Vector3, Building> _buildings;

    private const float yOffset = 0.01f;
    private EconomyManager _economyManager;

    // Use this for initialization
    void Start () {
        _buildings = new Dictionary<Vector3, Building>();
        _economyManager = FindObjectOfType<EconomyManager>();
        if (_economyManager == null)
        {
            Debug.LogError("Economy Manager not found");
        }
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
            Building referenceBuilding = GetSelectedBuilding();
            Building buildingCopy = Instantiate(referenceBuilding, transform);
            buildingCopy.transform.position = new Vector3 (pos.x, yOffset, pos.z);
            _buildings.Add(pos, buildingCopy);
            _economyManager.SpendGold(referenceBuilding.cost);
        }
    }

    public bool CanBuildAtPos(Vector3 pos)
    {
        Building referenceBuilding = referenceBuildings[currentBuildingIndex];
        bool res = true;
        res = res & !IsPosOccupied(pos);
        res = res & !IsYTooHigh(pos);
        res = res & _economyManager.CanSpendGold(GetSelectedBuilding().cost);
        return res;
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

    public void ChangeBuildingTool(int index)
    {
        if (index < referenceBuildings.Length)
        {
            currentBuildingIndex = index;
        }
    }

    public Building GetSelectedBuilding()
    {
        return referenceBuildings[currentBuildingIndex];
    }
}
