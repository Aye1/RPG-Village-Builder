using System.Collections;

public class Room {

    private ArrayList _layers;
    private ArrayList _dynamicObjects;
    private int _sizeX;
    private int _sizeY;
    private int _posX;
    private int _posY;

    private string _name;

    private bool _doorLeftBot;
    private bool _doorLeftTop;
    private bool _doorRightBot;
    private bool _doorRightTop;
    private bool _holeTopLeft;
    private bool _holeTopRight;
    private bool _holeBottomLeft;
    private bool _holeBottomRight;

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
        _doorLeftBot = (number & 128) == 128;
        _doorLeftTop = (number & 64) == 64;
        _holeTopLeft = (number & 32) == 32;
        _holeTopRight = (number & 16) == 16;
        _doorRightTop = (number & 8) == 8;
        _doorRightBot = (number & 4) == 4;
        _holeBottomRight = (number & 2) == 2;
        _holeBottomLeft = (number & 1) == 1;
    }
}
