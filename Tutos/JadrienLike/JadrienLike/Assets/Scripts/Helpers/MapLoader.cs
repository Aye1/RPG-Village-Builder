using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class MapLoader {

	private TextAsset _rawAsset;
	private ArrayList _rawLayers;

	public ArrayList Layers
	{
		get
		{
			return _rawLayers;
		}
		set {}
	}

	public MapLoader(string filepath) 
	{
		_rawAsset = Resources.Load(filepath) as TextAsset;
		_rawLayers = new ArrayList();
		if (_rawAsset != null) 
		{
			Debug.Log ("Asset loaded");
			try 
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(_rawAsset.text);
				XmlNodeList layers = xmlDoc.SelectNodes("map/layer");
				foreach (XmlNode layer in layers) 
				{
					Debug.Log ("Loading layer..."); 
					_rawLayers.Insert(0, layer.InnerText);
				}
				Debug.Log ("Xml loading succeeded");
			} 
			catch (Exception e)
			{
				Debug.Log ("Xml loading failed");
			}

		} else 
		{
			Debug.Log ("Asset not loaded");
		}
	}
}
