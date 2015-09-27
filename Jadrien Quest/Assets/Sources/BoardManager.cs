using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public int rows = 10;
	public int cols = 40;

	public GameObject[] floorTiles;
	public GameObject[] skyTiles;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void InitialiseList()
	{
		gridPositions.Clear();
		for (int x = 0; x < cols; x++) 
		{
			for (int y = 0; y < rows; y++) 
			{
				gridPositions.Add (new Vector3(x,y,0f));
			}
		}
	}

	void BoardSetup ()
	{
		boardHolder = new GameObject("Board").transform;
		for (int x = 0; x < cols; x++) 
		{
			for (int y = 0; y < rows; y++) 
			{	
				GameObject toInstantiate;
				if (y <= 2)
				{
					toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
				} else {
					toInstantiate = skyTiles[Random.Range(0, skyTiles.Length)];
				}
				GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);
			}
		}
	}

	public void SetupScene()
	{
		BoardSetup();
		InitialiseList();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
