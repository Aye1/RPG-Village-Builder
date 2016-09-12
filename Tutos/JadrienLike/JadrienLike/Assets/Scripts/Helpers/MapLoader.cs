using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class MapLoader {

	private TextAsset _rawAsset;
	private ArrayList _rawLayers;
    private ArrayList _dynamicObjects;

	public ArrayList Layers
	{
		get
		{
			return _rawLayers;
		}
		set {}
	}

    /// <summary>
    /// Returns the list of objects that have a dynamic parameter
    /// </summary>
    public ArrayList DynamicObjects
    {
        get
        {
            return _dynamicObjects;
        }
        set {}
    }

	public MapLoader(string filepath) 
	{
		_rawAsset = Resources.Load(filepath) as TextAsset;
		_rawLayers = new ArrayList();
        _dynamicObjects = new ArrayList();
		if (_rawAsset != null) 
		{
			Debug.Log ("Asset loaded");
			try 
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(_rawAsset.text);
                LoadLayers(xmlDoc);
                LoadDynamicObjects(xmlDoc);
				Debug.Log ("Xml loading succeeded");
			} 
			catch 
			{
				Debug.Log ("Xml loading failed");
			}

		} else 
		{
			Debug.Log ("Asset not loaded");
		}
	}

    private void LoadLayers(XmlDocument xmlDoc)
    {
        XmlNodeList layers = xmlDoc.SelectNodes("map/layer");
        foreach (XmlNode layer in layers)
        {
            Debug.Log("Loading layer...");
            _rawLayers.Insert(0, layer.InnerText);
        }
    }

    private void LoadDynamicObjects(XmlDocument xmlDoc)
    {
        XmlNodeList objects = xmlDoc.SelectNodes("map/objectgroup/object");
        if (objects != null && objects.Count > 0)
        {
            foreach(XmlNode obj in objects)
            {
                int x = int.Parse(obj.Attributes["x"].InnerText) / 100;
                int y = int.Parse(obj.Attributes["y"].InnerText) / 100;
                // TODO: update for all properties
                XmlNode prop = obj.SelectSingleNode("properties/property");
                if (prop.Attributes["name"].InnerText.Equals("destination"))
                {
                    DynamicMapObject door = new DynamicMapObject("Door");
                    door.properties.Add("destination", prop.Attributes["value"].InnerText);
                    door.x = x;
                    door.y = y;
                    _dynamicObjects.Add(door);
                }
            }
        }
    }
}
