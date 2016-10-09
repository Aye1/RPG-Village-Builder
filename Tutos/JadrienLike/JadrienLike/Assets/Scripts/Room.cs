using System.Collections;

public class Room {

    private ArrayList _layers;
    private ArrayList _dynamicObjects;
    private int _sizeX;
    private int _sizeY;

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

    public Room (string name)
    {
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
    }
}
