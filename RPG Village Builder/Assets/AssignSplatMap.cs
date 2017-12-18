using UnityEngine;
using System;
using System.Linq; // used for Sum of array
using Assets.Scripts.Helpers;

public class AssignSplatMap : MonoBehaviour
{
    private MapInfosManager _mapInfosManager;
    private Terrain _terrain;

    void Start()
    {
        _mapInfosManager = GetComponentInParent<MapInfosManager>();
        _terrain = GetComponent<Terrain>();
        ObjectChecker.CheckNullity(_mapInfosManager, "MapInfosManager not found");
        ObjectChecker.CheckNullity(_terrain, "Terrain component not found");

        ResizeTerrain();
        ManageCaseTextureAndHeight();
    }

    private void ResizeTerrain()
    {
        TerrainData data = _terrain.terrainData;
        Vector3 mapSize = _mapInfosManager.mapSize;
        data.size = new Vector3(mapSize.x, mapSize.z, mapSize.y) * CoordinatesConverter.cellSize;
    }

    private void ManageCaseTextureAndHeight()
    {
        TerrainData terrainData = _terrain.terrainData;

        float[,,] splatmapData = new float[terrainData.alphamapWidth,
                                            terrainData.alphamapHeight,
                                            terrainData.alphamapLayers];
        Vector2 mapSize = _mapInfosManager.mapSize;
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                Vector2 casePos = new Vector2(i, j);
                int[,] textureIds = GetCasesInfos(casePos);
                FillCaseTexture(casePos, textureIds, terrainData, splatmapData);
                SetCaseTerrainHeight(casePos, terrainData);
            }
        }
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

    private int[,] GetCasesInfos(Vector2 pos)
    {
        int[,] cases = new int[3, 3];
        int x = (int)pos.x;
        int y = (int)pos.y;
        cases[0, 0] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x + 1, y - 1)));
        cases[0, 1] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x + 1, y)));
        cases[0, 2] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x + 1, y + 1)));
        cases[1, 0] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x, y - 1)));
        cases[1, 1] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x, y)));
        cases[1, 2] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x, y + 1)));
        cases[2, 0] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x - 1, y - 1)));
        cases[2, 1] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x - 1, y)));
        cases[2, 2] = GetTextureId(_mapInfosManager.GetMapTypeAtPos(new Vector2(x - 1, y + 1)));
        return cases;
    }

    private void FillCaseTexture(Vector2 pos, int[,] idTextures, TerrainData terrainData, float[,,] splatmapData)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;
        int cellSize = CoordinatesConverter.cellSize;

        for (int i = 0; i < cellSize; i++)
        {
            for (int j = 0; j < cellSize; j++)
            {
                float[] splatWeights = new float[terrainData.alphamapLayers];
                splatWeights[idTextures[1, 1]] = 1.0f;
                AddRandomInfluence(new Vector2(i, j), idTextures, splatWeights);
                float z = splatWeights.Sum();

                for (int k = 0; k < terrainData.alphamapLayers; k++)
                {
                    // Normalize so that sum of all texture weights = 1
                    splatWeights[k] /= z;
                    // Assign this point to the splatmap array
                    splatmapData[x * cellSize + i, y * cellSize + j, k] = splatWeights[k];
                }
            }
        }
    }

    private void AddRandomInfluence(Vector2 pos, int[,] idTextures, float[] splatWeights)
    {
        float perX = pos.x / CoordinatesConverter.cellSize;
        float perY = pos.y / CoordinatesConverter.cellSize;
        float thresh = 0.3f;

        bool lowX = perX < thresh;
        bool highX = perX > 1 - thresh;
        bool lowY = perY < thresh;
        bool highY = perY > 1 - thresh;

        if (highX)
        {
            //Top
            AddInfluence(idTextures[0, 1], splatWeights, perX);
            if (lowY)
            {
                //Top Left
                AddInfluence(idTextures[0, 0], splatWeights, perX * (1 - perY));
            } else if (highY)
            {
                //Top right
                AddInfluence(idTextures[0, 2], splatWeights, perX * perY);
            }
        }
        if (lowX)
        {
            //Bottom
            AddInfluence(idTextures[2, 1], splatWeights, 1 - perX);
            if (lowY)
            {
                //Bottom Left
                AddInfluence(idTextures[2, 0], splatWeights, (1 - perX) * (1 - perY));
            }
            else if (highY)
            {
                //Bottom right
                AddInfluence(idTextures[2, 2], splatWeights, (1 - perX) * perY);
            }
        }
        if (lowY)
        {
            //Left
            AddInfluence(idTextures[1, 0], splatWeights, 1 - perY);
        } else if (highY)
        {
            //Right            
            AddInfluence(idTextures[1, 2], splatWeights, perY);
        }
    }

    private void AddInfluence(int id, float[] splatWeights, float variation)
    {
        float influence = 4.0f;
        float random = UnityEngine.Random.value;
        if (id != -1 && random < 0.5f)
        {
            splatWeights[id] += influence = variation;
        }
    }

    private void SetCaseTerrainHeight(Vector2 pos, TerrainData terrainData)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;
        int cellSize = CoordinatesConverter.cellSize;
        float[,] heights = new float[cellSize, cellSize];

        float[,] totalHeights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);

        CaseInfos infos = _mapInfosManager.GetInfosAtPos(new Vector2(x,y));
        if (infos != null)
        {
            for (int i = 0; i < cellSize; i++)
            {
                for (int j = 0; j < cellSize; j++)
                {
                    int globalX = x + i;
                    int globalY = y + j;
                    // Normalise x/y coordinates to range 0-1 
                    float y_01 = globalY / (float)terrainData.alphamapHeight;
                    float x_01 = globalX / (float)terrainData.alphamapWidth;

                    float newHeight = 0.01f;

                    if (infos.mapType == MapInfosManager.MapType.Water)
                    {
                        newHeight = 0.0f;
                    } else if (infos.mapType == MapInfosManager.MapType.Sand)
                    {
                        newHeight = 0.005f;
                    }

                    heights[j, i] = newHeight;
                    totalHeights[y + j, x + i] = newHeight;
                }
            }
            terrainData.SetHeights(y*cellSize, x*cellSize, heights);
            //terrainData.SetHeights(0,0, totalHeights);
        }
    }

    private void OldAssignTextures()
    {
        // Get a reference to the terrain data
        TerrainData terrainData = _terrain.terrainData;

        // Splatmap data is stored internally as a 3d array of floats, so declare a new empty array ready for your custom splatmap data:
        float[,,] splatmapData = new float[terrainData.alphamapWidth, 
            terrainData.alphamapHeight, 
            terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                // Normalise x/y coordinates to range 0-1 
                float y_01 = (float)y / (float)terrainData.alphamapHeight;
                float x_01 = (float)x / (float)terrainData.alphamapWidth;

                // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                //float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapHeight), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));

                // Calculate the normal of the terrain (note this is in normalised coordinates relative to the overall terrain dimensions)
                //Vector3 normal = terrainData.GetInterpolatedNormal(y_01, x_01);

                // Calculate the steepness of the terrain
               // float steepness = terrainData.GetSteepness(y_01, x_01);

                // Setup an array to record the mix of texture weights at this point
                float[] splatWeights = new float[terrainData.alphamapLayers];

                // CHANGE THE RULES BELOW TO SET THE WEIGHTS OF EACH TEXTURE ON WHATEVER RULES YOU WANT
                CaseInfos caseInfo = _mapInfosManager.GetInfosAtPos(new Vector2(x, y));
                CaseInfos infoXPlus = _mapInfosManager.GetInfosAtPos(new Vector2(x + 10, y));
                CaseInfos infoXMoins = _mapInfosManager.GetInfosAtPos(new Vector2(x - 10, y));
                CaseInfos infoYPlus = _mapInfosManager.GetInfosAtPos(new Vector2(x, y + 10));
                CaseInfos infoYMoins = _mapInfosManager.GetInfosAtPos(new Vector2(x, y - 10));

                int idCase = GetTextureId(caseInfo);
                int idXPlus = GetTextureId(infoXPlus);
                int idXMoins = GetTextureId(infoXMoins);
                int idYPlus = GetTextureId(infoYPlus);
                int idYMoins = GetTextureId(infoYMoins);

                if (caseInfo == null)
                {
                    //Debug.LogWarning("Case infos not found at pos (" + x + "," + y + ")");
                    splatWeights[0] = 0.5f;
                    splatWeights[1] = 0.5f;
                }
                else
                {
                    int yunit = y - (y / 10)*10;
                    int xunit = x - (x / 10)*10;

                    float currentRatio;

                    if (xunit < 4 && xunit != 0 && idXMoins != -1 && idXMoins != idCase)
                    {
                        currentRatio = (-2 * xunit + 7) / 10.0f;
                        splatWeights[idXMoins] = currentRatio + splatWeights[idXMoins] - splatWeights[idXMoins] * currentRatio;
                    }
                    else if (xunit > 7 && idXPlus != -1 && idXPlus != idCase)
                    {
                        currentRatio = (2 * xunit - 15) / 10.0f;
                        splatWeights[idXPlus] = currentRatio + splatWeights[idXPlus] - splatWeights[idXPlus] * currentRatio;
                    }
                    else if (xunit == 0 && idXPlus != -1 && idXPlus != idCase)
                    {
                        currentRatio = 0.49f;
                        splatWeights[idXPlus] = currentRatio + splatWeights[idXPlus] - splatWeights[idXPlus] * currentRatio;

                    }

                    if (yunit < 4 && yunit != 0 && idYMoins != -1 && idYMoins != idCase)
                    {
                        currentRatio = (-2 * yunit + 7) / 10.0f;
                        splatWeights[idYMoins] = currentRatio + splatWeights[idYMoins] - splatWeights[idYMoins] * currentRatio;

                    }
                    else if (yunit > 7 && idYPlus != -1 && idYPlus != idCase)
                    {
                        currentRatio = (2 * yunit - 15) / 10.0f;
                        splatWeights[idYPlus] = currentRatio + splatWeights[idYPlus] - splatWeights[idYPlus] * currentRatio;

                    }
                    else if (yunit == 0 && idYPlus != -1 && idYPlus != idCase)
                    {
                        currentRatio = 0.50f;
                        splatWeights[idYPlus] = currentRatio + splatWeights[idYPlus] - splatWeights[idYPlus] * currentRatio;

                    }
                    splatWeights[idCase] = splatWeights[idCase] == 0 ? 1 - splatWeights.Sum() : splatWeights[idCase] * (1 - splatWeights.Sum());
                }


                // Texture[2] stronger on flatter terrain
                // Note "steepness" is unbounded, so we "normalise" it by dividing by the extent of heightmap height and scale factor
                // Subtract result from 1.0 to give greater weighting to flat surfaces
                //splatWeights[2] = 1.0f - Mathf.Clamp01(steepness * steepness / (terrainData.heightmapHeight / 5.0f));

                // Texture[3] increases with height but only on surfaces facing positive Z axis 
                //splatWeights[3] = height * Mathf.Clamp01(normal.z);

                // Sum of all textures weights must add to 1, so calculate normalization factor from sum of weights
                float z = splatWeights.Sum();

                // Loop through each terrain texture
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {
                    // Normalize so that sum of all texture weights = 1
                    splatWeights[i] /= z;

                    // Assign this point to the splatmap array
                    splatmapData[x, y, i] = splatWeights[i];
                }   
            }
        }

        // Finally assign the new splatmap to the terrainData:
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

    private int GetTextureId(CaseInfos caseInfos)
    {
        if (caseInfos == null)
        {
            return -1;
        }
        return GetTextureId(caseInfos.mapType);
    }
    private int GetTextureId(MapInfosManager.MapType mapType)
    {
        switch (mapType)
        {
            case MapInfosManager.MapType.Grass:
                return 0;
            case MapInfosManager.MapType.Mountain:
                return 1;
            case MapInfosManager.MapType.Sand:
                return 2;
            case MapInfosManager.MapType.Water:
                return 3;
            default:
                return -1;
        }
    }
}
