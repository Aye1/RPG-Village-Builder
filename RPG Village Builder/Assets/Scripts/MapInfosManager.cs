using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapInfosManager : MonoBehaviour { 

    // Dictionary which contains infos for each case of the map
    // Information are :
    // - type of the case
    // - height of the case
    public Dictionary<Vector2, CaseInfos> infos;
    public Vector3 mapSize;

    public enum MapType { Grass, Mountain, Water, Sand };

    void Awake()
    {
        mapSize = new Vector3(300.0f, 500.0f, 600.0f);
        GenerateRandomMap();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitDebugMap()
    {
        int x = (int)mapSize.x;
        int y = (int)mapSize.y;
        infos = new Dictionary<Vector2, CaseInfos>();
        for (int i=0; i<x; i++)
        {
            for (int j=0; j<y; j++)
            {
                Vector2 pos = new Vector2(i, j);
                MapType mapType = (i <= 50) ^ (j <= 50) ? MapType.Grass : MapType.Mountain;
                CaseInfos caseInfos = new CaseInfos(mapType, 0.0f);
                infos.Add(pos, caseInfos);
            }
        }
    }

    private void GenerateRandomMap()
    {
        infos = new Dictionary<Vector2, CaseInfos>();
        InitMapWithGrass();
        GenerateRiver();
    }

    private void InitMapWithGrass()
    {
        for (int i=0; i<mapSize.x; i++)
        {
            for (int j=0; j<mapSize.y; j++)
            {
                Vector2 pos = new Vector2(i, j);
                CaseInfos caseInfos = new CaseInfos(MapType.Grass, 0.0f);
                infos.Add(pos, caseInfos);
            }
        }
    }
    private void GenerateRiver()
    {
        int width = 50;
        int beachWidth = 10;
        int currentX = 0;
        int currentY = 75;

        while (currentX <= mapSize.x)
        {
            for (int i = 0; i < beachWidth; i++)
            {
                Vector2 pos = new Vector2(currentX, currentY - i);
                infos[pos].mapType = MapType.Sand;
            }
            for (int i = 0; i < width; i++)
            {
                Vector2 pos = new Vector2(currentX, currentY + i);
                infos[pos].mapType = MapType.Water;
            }
            for (int i = 0; i < beachWidth; i++)
            {
                Vector2 pos = new Vector2(currentX, currentY + i + width);
                infos[pos].mapType = MapType.Sand;
            }
            bool dirChoice = Random.value <= 0.5f;
            if (dirChoice)
            {
                currentX++;
            }
            else
            {
                currentY++;
            }
        }
    }

    public CaseInfos GetInfosAtPos(Vector2 pos)
    {
        CaseInfos res;
        infos.TryGetValue(pos, out res);
        return res;
    }
}
