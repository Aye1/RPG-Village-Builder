using UnityEngine;
using System.Collections;

public class IntCouple {

	#region Accessors
	private int _x;
	private int _y;
	
	public int X 
	{
		get { return _x; }
		set { 
			_x = value;
		}
	}
	
	public int Y 
	{
		get { return _y; }
		set { 
			_y = value;
		}
	}
	#endregion

	public IntCouple(int x, int y)
	{
		X = x;
		Y = y;
	}

}
