using UnityEngine;
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
        AssignTextures();
    }

    private void ResizeTerrain()
    {
        TerrainData data = _terrain.terrainData;
        data.size = new Vector3(_mapInfosManager.mapSize.x, _mapInfosManager.mapSize.z, _mapInfosManager.mapSize.y);
    }

    private void AssignTextures()
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
        switch(caseInfos.mapType)
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

    // TODO: factorize conditions
    private float GetInfluence(CaseInfos caseInfos, MapInfosManager.MapType type)
    {
        if (caseInfos == null)
        {
            return 0.0f;
        }
        if (caseInfos.mapType == type)
        {
            return 1.0f;
        }
        return 0.0f;
    }
}
