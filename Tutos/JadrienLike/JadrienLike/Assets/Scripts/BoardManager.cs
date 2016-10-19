using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System;

public class BoardManager : MonoBehaviour {

    #region Unity debug variables
    [Header("Debug")]
    public bool debugMode = false;
    public int debugZoneId = 0;
    #endregion

    #region Tiles
    [Header("Tiles")]
    public GameObject[] topTiles;
	public GameObject[] bottomTiles;
	public GameObject[] leftTiles;
	public GameObject[] rightTiles;
	public GameObject[] fullTiles;
	public GameObject[] cornerbgTiles;
	public GameObject[] cornerbdTiles;
	public GameObject[] cornerhgTiles;
	public GameObject[] cornerhdTiles;
	public GameObject[] cliffbgTiles;
	public GameObject[] cliffbdTiles;
	public GameObject[] cliffhgTiles;
	public GameObject[] cliffhdTiles;
	public GameObject[] backgroundTiles;
    public Door[] exitTiles;
    public GameObject   ladderTile;
    public GameObject   leftChair;
    public GameObject[] oneWayPlatformTiles;
    #endregion

    #region Enemies
    [Header("Enemies")]
    public Enemy[] clockEnemies;
    public Enemy[] dumbEnemies;
    public Enemy[] cardEnemies;
    public Enemy timeBoss;
    public Enemy timeBossClock;
    #endregion

    #region Collectibles
    [Header("Collectibles")]
    public GameObject[] coins;
    public GameObject[] redPotions;
    public GameObject[] bluePotions;
    public GameObject[] drinkMePotions;
    #endregion

    #region Private variables
    private Vector3 _initPlayerPosition;
    private int _zoneId = 0;
    private Dictionary<int, Zone> _zonesDico;
    private Transform boardHolder;
    private Room currentRoom;
    #endregion

    #region Accessors
    public Vector3 InitPlayerPosition
    {
        get
        {
            return _initPlayerPosition;
        }
        set
        {
            if (value != null)
            {
                _initPlayerPosition = value;
            }
        }
    }

    public int ZoneId
    {
        get
        {
            return _zoneId;
        }
        set
        {
            if (value != _zoneId && value < _zonesDico.Count)
            {
                _zoneId = value;
                ChangeZoneLayout();
            }
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
        // Warning: may not be called (don't know why)
    }
    
	// Update is called once per frame
	void Update () {

        // Warning: may not be called (don't know why)
	}

    private void ChangeZoneLayout()
    {
        Debug.Log("Switching to zone layout " + ZoneId);
        SpriteRenderer[] children = boardHolder.GetComponentsInChildren<SpriteRenderer>(); 
        Zone zone = null;
        if (!_zonesDico.ContainsKey(ZoneId))
        {
            Debug.Log("Invalid id");
            return;
        }
        _zonesDico.TryGetValue(ZoneId, out zone);

        if (zone == null)
        {
            Debug.Log("Could not load zone");
            return;
        }
        if (children == null)
        {
            Debug.Log ("No child found");
        }
        else
        {
            Debug.Log("Children count: " + children.Length);
        }
        
        foreach (SpriteRenderer child in children)
        {
            Sprite sprite = child.sprite;
            if (sprite.name.StartsWith("background"))
            {
                child.sprite = zone.GetBackgroundSprite();
            } 
            else if (sprite.name.StartsWith("bottom"))
            {
                child.sprite = zone.GetBottomSprite();
            }
            else if (sprite.name.StartsWith("cliff_bl"))
            {
                child.sprite = zone.GetCliffBLSprite();
            }
            else if (sprite.name.StartsWith("cliff_br"))
            {
                child.sprite = zone.GetCliffBRSprite();
            }
            else if (sprite.name.StartsWith("cliff_tl"))
            {
                child.sprite = zone.GetCliffTLSprite();
            }
            else if (sprite.name.StartsWith("cliff_tr"))
            {
                child.sprite = zone.GetCliffTRSprite();
            }
            else if (sprite.name.StartsWith("corner_bl"))
            {
                child.sprite = zone.GetCornerBLSprite();
            }
            else if (sprite.name.StartsWith("corner_br"))
            {
                child.sprite = zone.GetCornerBRSprite();
            }
            else if (sprite.name.StartsWith("corner_tl"))
            {
                child.sprite = zone.GetCornerTLSprite();
            }
            else if (sprite.name.StartsWith("corner_tr"))
            {
                child.sprite = zone.GetCornerTRSprite();
            }
            else if (sprite.name.StartsWith("exit"))
            {
                child.sprite = zone.GetExitSprite();
            }
            else if (sprite.name.StartsWith("full"))
            {
                child.sprite = zone.GetFullSprite();
            }
            else if (sprite.name.StartsWith("left"))
            {
                child.sprite = zone.GetLeftSprite();
            }
            else if (sprite.name.StartsWith("right"))
            {
                child.sprite = zone.GetRightSprite();
            }
            else if (sprite.name.StartsWith("top"))
            {
                child.sprite = zone.GetTopSprite();
            } 
            else
            {
                // Do nothing, we don't know you, tile!
            }
        }
        
    }
    
    /// <summary>
    /// Loads all the zones from the tiles
    /// </summary>
    private void CreateZones() 
    {
        // Only create zones if necessary
        if (_zonesDico == null) 
        {
            _zonesDico = new Dictionary<int, Zone>();
            Zone currentZone;
            int i = 0;
            foreach (string file in System.IO.Directory.GetFiles("Assets/Resources/Tiles"))
            { 
                Debug.Log ("Zone at path " + file);
                if (file.EndsWith(".png"))
                {
                    string[] parts = file.Split(new char[]{'/', '\\'});
                    string filename = parts[parts.Length-1];
                    filename = filename.Replace(".png", "");
                    currentZone = new Zone(i, "Tiles/"+filename);
                    _zonesDico.Add(i, currentZone);
                    i++;
                }
            }
           
            if (ZoneId >= _zonesDico.Count)
            {
                ZoneId = 0;
            }
        }
    }

    /// <summary>
    /// Deletes all children of the board.
    /// </summary>
    public void EmptyBoard()
    {
        int removeCount = 0;
        // First loading, you can't empty the board
        if (boardHolder == null)
        {
            return;
        }

        foreach (Transform child in boardHolder.transform)
        {
            Destroy(child.gameObject);
            removeCount++;
        }
        Debug.Log("Elements removed from board: " + removeCount);
    }

    public void InstantiateRoom(Room room, Vector3 offset)
    {
        CreateZones();
        // First board creation
        if (boardHolder == null)
        {
            boardHolder = new GameObject("Room").transform;
        }

        float xOffset = offset.x;
        float yOffset = offset.y;

        room.PosX = (int) xOffset / RoomManager.RoomWidth;
        room.PosY = (int) yOffset / RoomManager.RoomHeight;

        float currentZ = 0;
        GameObject toInstantiate = null;

        foreach (ArrayList layer in room.Layers)
        {
            for (int y = 0; y < room.SizeY; y++)
            {
                ArrayList currentRow = layer.ToArray()[y] as ArrayList;
                for (int x = 0; x < room.SizeX; x++)
                {
                    toInstantiate = null;
                    string elem = currentRow.ToArray()[x] as string;
                    switch (elem)
                    {
                        case "0":
                            break;
                        case "1":
                            // toInstantiate = backgroundTiles[Random.Range(0, backgroundTiles.Length)];
                            break;
                        case "2":
                            toInstantiate = cliffhgTiles[Random.Range(0, cliffhgTiles.Length)];
                            break;
                        case "3":
                            toInstantiate = cliffbgTiles[Random.Range(0, cliffbgTiles.Length)];
                            break;
                        case "4":
                            toInstantiate = fullTiles[Random.Range(0, fullTiles.Length)];
                            break;
                        case "5":
                            toInstantiate = cornerhgTiles[Random.Range(0, cornerhgTiles.Length)];
                            break;
                        case "6":
                            toInstantiate = topTiles[Random.Range(0, topTiles.Length)];
                            break;
                        case "7":
                            toInstantiate = cornerhdTiles[Random.Range(0, cornerhdTiles.Length)];
                            break;
                        case "8":
                            toInstantiate = exitTiles[Random.Range(0, exitTiles.Length)].gameObject;
                            //throw new Exception("Doors are managed dynamically now");
                            break;
                        case "9":
                            toInstantiate = leftTiles[Random.Range(0, leftTiles.Length)];
                            break;
                        case "10":
                            _initPlayerPosition = new Vector3(x + xOffset, room.SizeY - y + yOffset, 0f);
                            break;
                        case "11":
                            toInstantiate = rightTiles[Random.Range(0, rightTiles.Length)];
                            break;
                        case "12":
                            toInstantiate = cliffhdTiles[Random.Range(0, cliffhdTiles.Length)];
                            break;
                        case "13":
                            toInstantiate = cornerbgTiles[Random.Range(0, cornerbgTiles.Length)];
                            break;
                        case "14":
                            toInstantiate = bottomTiles[Random.Range(0, bottomTiles.Length)];
                            break;
                        case "15":
                            toInstantiate = cornerbdTiles[Random.Range(0, cornerbdTiles.Length)];
                            break;
                        case "16":
                            toInstantiate = cliffbdTiles[Random.Range(0, cliffbdTiles.Length)];
                            break;
                        case "17":
                            toInstantiate = clockEnemies[Random.Range(0, clockEnemies.Length)].gameObject;
                            break;
                        case "18":
                            toInstantiate = dumbEnemies[Random.Range(0, dumbEnemies.Length)].gameObject;
                            break;
                        case "19":
                            toInstantiate = redPotions[Random.Range(0, redPotions.Length)];
                            break;
                        case "20":
                            toInstantiate = bluePotions[Random.Range(0, bluePotions.Length)];
                            break;
                        case "21":
                            toInstantiate = coins[Random.Range(0, coins.Length)];
                            break;
                        case "22":
                            toInstantiate = cardEnemies[Random.Range(0, cardEnemies.Length)].gameObject;
                            break;
                        case "23":
                            toInstantiate = ladderTile;
                            break;
                        case "24":
                            toInstantiate = drinkMePotions[Random.Range(0, drinkMePotions.Length)];
                            break;
                        case "25":
                            toInstantiate = timeBoss.gameObject;
                            break;
                        case "26":
                            toInstantiate = timeBossClock.gameObject;
                            break;
                        case "27":
                            toInstantiate = leftChair;
                            break;
                        case "28":
                            toInstantiate = oneWayPlatformTiles[Random.Range(0, oneWayPlatformTiles.Length)];
                            break;
                        default:
                            toInstantiate = backgroundTiles[Random.Range(0, backgroundTiles.Length)];
                            break;
                    }
                    if (toInstantiate != null)
                    {
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x + xOffset, room.SizeY - y + yOffset, toInstantiate.transform.position.z), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);

                        // Enemies are added to the room
                        if (instance.GetComponent<Enemy>() != null)
                        {
                            room.Enemies.Add(instance);
                        }
                    }
                }
            }
            currentZ += 0.1f;
        }
        InstantiateDynamicObjects(room);
        ChangeZoneLayout();
    }

    private void InstantiateDynamicObjects(Room room)
    {
        //TODO: this code should be more generic
        foreach (DynamicMapObject obj in room.DynamicObjects)
        {
            if (obj.ObjectType == MapObjectType.Door)
            {
                Door toInstantiate = exitTiles[Random.Range(0, exitTiles.Length)];
                toInstantiate.destination = obj.properties["destination"];
                if (toInstantiate != null)
                {
                    GameObject instance = Instantiate(toInstantiate.gameObject, new Vector3(obj.x, room.SizeY - obj.y, toInstantiate.transform.position.z), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    public void LoadRoom(Room room, Vector3 offset)
    {
        currentRoom = room;
        InstantiateRoom(room, offset);
    }

}

