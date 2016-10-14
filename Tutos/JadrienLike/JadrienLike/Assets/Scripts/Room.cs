using System.Collections;
using UnityEngine;

public class Room {

    private ArrayList _layers;
    private ArrayList _dynamicObjects;
    private int _sizeX;
    private int _sizeY;
    private int _posX;
    private int _posY;
    private ArrayList _enemies;

    private string _name;

    public bool doorLeftBot;
    public bool doorLeftTop;
    public bool doorRightBot;
    public bool doorRightTop;
    public bool holeTopLeft;
    public bool holeTopRight;
    public bool holeBottomLeft;
    public bool holeBottomRight;

    #region Accessors
    /// <summary>
    /// Gets or sets the different layers of the map
    /// </summary>
    /// <value>The layers of the map.</value>
    public ArrayList Layers
    {
        get { return _layers; }
        set
        {
            if (value != null)
            {
                _layers = value;
                ArrayList firstLayer = _layers.ToArray()[0] as ArrayList;
                SizeY = firstLayer.Count;
                ArrayList firstRow = firstLayer.ToArray()[0] as ArrayList;
                SizeX = firstRow.Count;
            }
        }
    }

    /// <summary>
    /// Gets or sets the dynamic objects of the map (e.g. doors)
    /// </summary>
    public ArrayList DynamicObjects
    {
        get { return _dynamicObjects; }
        set
        {
            if (value != null)
            {
                _dynamicObjects = value;
            }
        }
    }

    /// Gets or sets the size x.
    /// </summary>
    /// <value>The size x.</value>
    public int SizeX
    {
        get { return _sizeX; }
        set
        {
            if (value >= 1)
                _sizeX = value;
        }
    }

    /// <summary>
    /// Gets or sets the size y.
    /// </summary>
    /// <value>The size y.</value>
    public int SizeY
    {
        get { return _sizeY; }
        set
        {
            if (value >= 1)
                _sizeY = value;
        }
    }

    /// <summary>
    /// Position X of the room on the board (in number of rooms)
    /// </summary>
    public int PosX
    {
        get
        {
            return _posX;
        }
        set
        {
            if (_posX != value)
            {
                _posX = value;
            }
        }
    }

    /// <summary>
    /// Position Y of the room on the board (in number of rooms)
    /// </summary>
    public int PosY
    {
        get
        {
            return _posY;
        }
        set
        {
            if (_posY != value)
            {
                _posY = value;
            }
        }
    }

    /// <summary>
    /// List of enemies in the room
    /// </summary>
    public ArrayList Enemies
    {
        get
        {
            if (_enemies == null)
            {
                _enemies = new ArrayList();
            }
            return _enemies;
        }
        set
        {
            if (value != _enemies)
            {
                _enemies = value;
            }
        }
    }
    #endregion

    public Room (string name)
    {
        _name = name;
        MapLoader loader = new MapLoader(name);
        ArrayList layers = loader.Layers;
        ArrayList parsedLayers = new ArrayList();
        CSVParser parser = CSVParser.Instance;

        foreach (string layer in layers)
        {
            parsedLayers.Add(CSVParser.ParseCSV(layer));
        }

        // Dynamic objects (e.g. doors)
        DynamicObjects = loader.DynamicObjects;
        if (parsedLayers.Count != 0)
        {
            Layers = parsedLayers;
        }
        LoadDoorInfo();
    }

    private void LoadDoorInfo()
    {
        int number = RoomNameParser.GetNumberFromFilename(_name);
        doorLeftBot = (number & 128) == 128;
        doorLeftTop = (number & 64) == 64;
        holeTopLeft = (number & 32) == 32;
        holeTopRight = (number & 16) == 16;
        doorRightTop = (number & 8) == 8;
        doorRightBot = (number & 4) == 4;
        holeBottomRight = (number & 2) == 2;
        holeBottomLeft = (number & 1) == 1;
    }

    public void OnPlayerExit()
    {
        foreach(GameObject enemy in Enemies)
        {
            enemy.gameObject.SetActive(false);
            Debug.Log("Deactivating enemy "+ enemy+ " in room (" + PosX + "," + PosY + ")");
        }
    }

    public void OnPlayerEnter()
    {
        foreach(GameObject enemy in Enemies)
        {
            enemy.gameObject.SetActive(true);
            Debug.Log("Reactivating enemy "+ enemy+ " in room (" + PosX + "," + PosY + ")");
        }
    }
}
