using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

    #region Unity debug variables
    public bool debugMode = false;
    public int debugZoneId = 0;
    #endregion

    #region Tiles
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
    public GameObject[] exitTiles;
    #endregion

    #region Enemies
    public GameObject[] clockEnemies;
    public GameObject[] dumbEnemies;
    public GameObject[] cardEnemies;
    #endregion

    #region Collectibles
    public GameObject[] coins;
    public GameObject[] redPotions;
    public GameObject[] bluePotions;
    #endregion

    #region Private variables
    private Vector3 _initPlayerPosition;

    private int minX = 20;
	private int maxX = 40;
	private int minY = 20;
	private int maxY = 40;

    private int _zoneId = 0;
    private Zone[] _zones;
    #endregion

    private Transform boardHolder;

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
            if (value != _zoneId && value < _zones.Length)
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
        Zone zone = _zones[ZoneId];
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
    
    private void CreateZones() 
    {
        if (_zones == null) 
        {
            Zone basicZone = new Zone(0, "Tiles/spritesheet-basic");
            Zone reverseZone = new Zone(1, "Tiles/spritesheet-reverse");
            _zones = new Zone[2];
            _zones[0] = basicZone;
            _zones[1] = reverseZone;
            if (ZoneId >= _zones.Length)
            {
                ZoneId = 0;
            }
        }
    }

	public Board CreateBoard() {
        // First line may be removed
		IntCouple boardsize = GenerateBoardSize();
		Board board = new Board(boardsize);
		InstantiateBoard(board);
		return board;
	}
	
    [Obsolete("Old map generation, may be removed")]
	private IntCouple GenerateBoardSize() {
		int x = (int) Random.Range(minX, maxX);
		int y = (int) Random.Range(minY, maxY);
		
		Board board = new Board(x,y);
		return new IntCouple(x, y);
	}
	
	/*public void InstantiateBoard(Board board) {
		boardHolder = new GameObject("Board").transform;
		board.BoardHolder = boardHolder;
		//int xOffset = 0;

		// On peut perdre une case de longueur, on s'en fout pour le moment
		int boundX = board.SizeX / 2;
		for (int x = -boundX; x < boundX ; x++) {
			for (int y = 0; y < board.SizeY ; y++) {
				GameObject bgToInstantiate = backgroundTiles[Random.Range(0, backgroundTiles.Length)];
				GameObject bgInstance = Instantiate(bgToInstantiate, new Vector3(x,y,1.0f), Quaternion.identity) as GameObject;
				bgInstance.transform.SetParent(boardHolder);

				GameObject toInstantiate;
				if (x == -boundX && y == 0)
				{
					toInstantiate = cornerbgTiles[Random.Range(0, cornerbgTiles.Length)];
				} else if (x == boundX-1 && y == 0)
				{
					toInstantiate = cornerbdTiles[Random.Range(0, cornerbdTiles.Length)];
				} else if (x == -boundX && y == board.SizeY-1)
				{
					toInstantiate = cornerhgTiles[Random.Range(0, cornerhgTiles.Length)];
				} else if (x == boundX-1 && y == board.SizeY-1)
				{
					toInstantiate = cornerhdTiles[Random.Range(0, cornerhdTiles.Length)];
				} else if (x == -boundX)
				{
					toInstantiate = leftTiles[Random.Range(0, leftTiles.Length)];
				} else if (x == boundX-1)
				{
					toInstantiate = rightTiles[Random.Range(0, rightTiles.Length)];
				} else if (y == 0)
				{
					toInstantiate = bottomTiles[Random.Range(0, bottomTiles.Length)];
				} else if (y == board.SizeY-1)
				{
					toInstantiate = topTiles[Random.Range(0, topTiles.Length)];
				} else {
					toInstantiate = null;
				}
				if (toInstantiate != null)
				{
					GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder); 
				}
			}
		}
	}*/

	public void InstantiateBoard(Board board) 
	{
        CreateZones();
		boardHolder = new GameObject("Board").transform;
		board.BoardHolder = boardHolder;

        int xOffset = 0;
        int yOffset = 0;
        float currentZ = 0;
		GameObject toInstantiate = null;

		foreach (ArrayList layer in board.Layers) 
		{
			for (int y = 0; y < board.SizeY; y++) 
			{
				ArrayList currentRow = layer.ToArray()[y] as ArrayList;
				for (int x = 0; x < board.SizeX; x++)
				{
					toInstantiate = null;
					string elem = currentRow.ToArray()[x] as string;
					switch(elem) 
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
                            Debug.Log("Case 8 found!");
						    break;
					    case "9":
						    toInstantiate = leftTiles[Random.Range(0, leftTiles.Length)];
						    break;
					    case "10":
                            _initPlayerPosition = new Vector3(x + xOffset, board.SizeY - y + yOffset, 0f);
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
                            toInstantiate = clockEnemies[Random.Range(0, clockEnemies.Length)];
                            break;
                        case "18":
                            toInstantiate = dumbEnemies[Random.Range(0, dumbEnemies.Length)];
                            toInstantiate.transform.Rotate(new Vector3(0, 0, 90));
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
                            toInstantiate = cardEnemies[Random.Range(0, cardEnemies.Length)];
                            break;
                        default:
						toInstantiate = backgroundTiles[Random.Range(0, backgroundTiles.Length)];
						break;
					}
					if (toInstantiate != null) 
					{
						GameObject instance = Instantiate(toInstantiate, new Vector3(x+xOffset,board.SizeY-y+yOffset,0.0f), Quaternion.identity) as GameObject;
						instance.transform.SetParent(boardHolder); 
					}
				}
			}
            currentZ += 0.1f;
		}
	}
}
