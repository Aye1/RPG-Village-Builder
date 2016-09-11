﻿using UnityEngine;
using System.Collections;

public class Board {
	
	#region Private attributes
	private const int defaultX = 10;
	private const int defaultY = 10;
	private int _sizeX;
	private int _sizeY;
	private Transform _boardHolder;
	private ArrayList _layers;
    private ArrayList _dynamicObjects;
	#endregion
	
	#region Accessors
	
	/// <summary>
	/// Gets or sets the size x.
	/// </summary>
	/// <value>The size x.</value>
	public int SizeX 
	{
		get { return _sizeX; }
		set { 
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
		set { 
			if (value >= 1)
				_sizeY = value;
		}
	}
	
	/// <summary>
	/// Gets or sets the board holder.
	/// </summary>
	/// <value>The board holder.</value>
	public Transform BoardHolder
	{
		get { return _boardHolder; }
		set { _boardHolder = value; }
	}

	/// <summary>
	/// Gets or sets the different layers of the map
	/// </summary>
	/// <value>The layers of the map.</value>
	public ArrayList Layers 
	{
		get { return _layers; }
		set { 
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
	#endregion
	
	#region Constructors
	/// <summary>
	/// Initializes a new instance of the <see cref="Board"/> class with default size.
	/// </summary>
	public Board() : this(defaultX, defaultY)
	{
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Board"/> class with a specified size.
	/// </summary>
	/// <param name="x">The length of the board.</param>
	/// <param name="y">The heigth of the board.</param>
	public Board(int x, int y) {
		SizeX = x;
		SizeY = y;
	//	_mapMatrix = new int[x*y];
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Board"/> class with a specified size.
	/// </summary>
	/// <param name="boardSize">Board size.</param>
	public Board(IntCouple boardSize) : this(boardSize.X, boardSize.Y)
	{
	}
	#endregion
}
