using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class MapLoader {

	private TextAsset _rawAsset;
	private string[] _rawLayers;

	public MapLoader(string filepath) 
	{
		_rawAsset = Resources.Load(filepath) as TextAsset;
		if (_rawAsset != null) 
		{
			Debug.Log ("Asset loaded");
			try 
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(_rawAsset.text);
				XmlNodeList layers = xmlDoc.SelectNodes("map/layer");
				_rawLayers = new string[layers.Count];
				int i = 0;
				foreach (XmlNode layer in layers) 
				{
					_rawLayers[i] = layer.InnerText;
					Debug.Log ("Loading layer..."); 
					i++;
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
