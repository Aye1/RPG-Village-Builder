using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    public GameObject posIndicator;
    private BuildingManager _buildingManager;

    private Building _ghostBuilding;
    private Building _toBeDestroyedBuilding;

    private const float yOffset = 0.01f;
    private int div = 20;

	// Use this for initialization
	void Start () {
        _buildingManager = FindObjectOfType<BuildingManager>();
        if (_buildingManager == null)
        {
            Debug.LogError("Building Manager not found.");
        }
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePosIndicator();
        ManageClick();
	}

    private void UpdatePosIndicator()
    {
        Vector3 hitPosition = GetMousePositionInWorld();
        if (hitPosition != Vector3.zero) {
            Vector3 pos = GetDiscreteMousePosition();
            posIndicator.transform.position = new Vector3(pos.x, yOffset, pos.z);
            UpdateIndicatorColor(pos);
            UpdateGhostBuilding(pos);
        }
    }

    private void UpdateGhostBuilding(Vector3 pos)
    {
        Building currentBuilding = _buildingManager.GetSelectedBuilding();
        if (_ghostBuilding == null || (currentBuilding.GetType() != _ghostBuilding.GetType()))
        {
            if (_ghostBuilding != null)
            {
                Destroy(_ghostBuilding.gameObject);
            }
            _ghostBuilding = Instantiate(currentBuilding, transform);
        }
        _ghostBuilding.transform.position = pos;
    }

    private Vector3 GetMousePositionInWorld()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        } 
        else
        {
            return Vector3.zero;
        }
    }
    
    private Vector3 GetDiscreteMousePosition()
    {
        Vector3 mousePos = GetMousePositionInWorld();
        if (mousePos == Vector3.zero)
        {
            return Vector3.zero;
        }
        return new Vector3(DiscreteCoord(mousePos.x, div) + div / 2, DiscreteCoord(mousePos.y, div), DiscreteCoord(mousePos.z, div));
    }

    private float DiscreteCoord(float coord, int div)
    {
        return (int)(coord / div) * div;
    } 

    private void ManageClick()
    {
        if(Input.GetMouseButtonDown(0) && GetMousePositionInWorld().y <= 0.1f)
        {
            _buildingManager.CreateBuilding(GetDiscreteMousePosition());
        }
    }

    private void UpdateIndicatorColor(Vector3 pos)
    {
        Color color = Color.red;
        if (_buildingManager.CanBuildAtPos(pos))
        {
            color = Color.green;
        }
        posIndicator.GetComponentInChildren<SpriteRenderer>().color = color;
    }
}
