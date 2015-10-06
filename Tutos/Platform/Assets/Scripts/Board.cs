using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	#region Private attributes
	private const int defaultX = 10;
	private const int defaultY = 10;
	private int _sizeX;
	private int _sizeY;
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
			if (_sizeX >= 2)
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
			if (_sizeY >= 2)
				_sizeY = value;
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
