using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTool : MonoBehaviour {

    public BasicTower tower;
    public Board board;

    public enum ToolState { None, AddTower };
    private ToolState toolState;
    private BasicTower brushTower;

    private readonly int gridSize = 20;

	// Use this for initialization
	void Start () {
        brushTower = Instantiate(tower);
        brushTower.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        updateBrushPosition();
        updateBrushColor();
        if (board != null)
        {
            if (Input.GetMouseButtonDown(0) && board.isPositionValid(Input.mousePosition))
            {
                BasicTower newTower = Instantiate(tower, board.transform);
                Vector3 pos = Input.mousePosition;
                pos = magnetPosToGrid(pos);
                pos = Camera.main.ScreenToWorldPoint(pos);
                pos.z = -1;
                newTower.transform.localPosition = pos;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rotateTool();
            }
        }
        tower.enabled = toolState == ToolState.AddTower;
	}

    private void updateBrushPosition()
    {
        Vector3 pos = Input.mousePosition;
        pos = magnetPosToGrid(pos);
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = -1;
        brushTower.transform.position = pos;
    }

    private void updateBrushColor()
    {
        if (board.isPositionValid(Input.mousePosition))
        {
            brushTower.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        } else
        {
            brushTower.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }

    private void rotateTool()
    {
        if (toolState == ToolState.None)
        {
            toolState = ToolState.AddTower;
        } else
        {
            toolState = ToolState.None;
        }
    }
    private Vector3 magnetPosToGrid(Vector3 pos)
    {
        Vector3 res = pos;
        res.x = ((int)res.x / gridSize) * gridSize + gridSize / 2;
        res.y = ((int)res.y / gridSize) * gridSize + gridSize / 2;
        return res;
    }
}
