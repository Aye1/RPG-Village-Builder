using UnityEngine;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using Random = UnityEngine.Random;
using System;

public class RoomManager : MonoBehaviour {

    public int _currentRoomId;
    public int _currentRoomX;
    public int _currentRoomY;

    public static readonly int RoomWidth = 18;
    public static readonly int RoomHeight = 10;
    private static readonly string RoomFolder = "Rooms/";

    private Player _player;
    private Camera _camera;
    private Camera_behaviour _camera_behaviour;
    private BoardManager _boardManager;
    private Dictionary<Vector2, bool> _rooms;

    // Use this for initialization
    void Start() {
        _currentRoomX = 0;
        _currentRoomY = 0;
        _player = FindObjectOfType<Player>();
        _camera = FindObjectOfType<Camera>();
        _camera_behaviour = _camera.GetComponentInChildren<Camera_behaviour>();
        _boardManager = FindObjectOfType<GameController>().boardManager;
        _rooms = new Dictionary<Vector2, bool>();
        // Add the first room to the dictionary of created rooms
        _rooms.Add(new Vector2(_currentRoomX, _currentRoomY), true);
    }

    // Update is called once per frame
    void Update() {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        bool shouldMove = false;
        Vector3 move = Vector3.zero;
        if (_player.transform.position.x > (_currentRoomX+1)*RoomWidth-0.5)
        {
            Debug.Log("Changement de pièce");
            shouldMove = true;
            _currentRoomX++;
            move = new Vector3(1.2f, 0.0f, 0.0f);
        }
        else if(_player.transform.position.x < _currentRoomX * RoomWidth + 0.5)
        {
            shouldMove = true;
            _currentRoomX--;
            move = new Vector3(-1.2f, 0.0f, 0.0f);
        }

        if (shouldMove)
        {
            Vector3 offset = new Vector3(_currentRoomX * RoomWidth, _currentRoomY * RoomHeight, 0.0f);
            if (!_rooms.ContainsKey(new Vector2(_currentRoomX, _currentRoomY)))
            {
                _boardManager.LoadRoom(SelectNextRoom(), offset);
                _rooms.Add(new Vector2(_currentRoomX, _currentRoomY), true);
            }
            MoveCamera();
        }
        MovePlayer(move);
    }

    private void MoveCamera()
    {
        Vector3 offset = new Vector3(_currentRoomX * RoomWidth, _currentRoomY * RoomHeight, 0.0f);
        _camera_behaviour.Offset = offset;
    }

    private void MovePlayer(Vector3 move)
    {
        _player.transform.position += move;
    }

    /// <summary>
    /// Selects which room should be loaded next, according to the current configuration
    /// </summary>
    /// <returns>The filename of the room to be loaded</returns>
    private string SelectNextRoom()
    {
        string nextRoomPath = "";
        List<string> pool = CreatePathPool(128);
        string selectedRoom = pool[Random.Range(0, pool.Count)];
        nextRoomPath = RoomFolder  + selectedRoom;
        return nextRoomPath;
    }

    /// <summary>
    /// Creates the list of rooms which have the correct options
    /// </summary>
    /// <param name="pattern">Pattern which defines the options</param>
    /// <returns>A list of the filenames of the correct rooms</returns>
    private List<string> CreatePathPool(int pattern)
    {
        List<string> pool = new List<string>();
        foreach(string file in System.IO.Directory.GetFiles("Assets/Resources/Rooms"))
        {
            if (!file.EndsWith(".meta"))
            {
                //string shortfile = file.Replace(".xml", "");
                //shortfile = shortfile.Replace("Assets/Resources/Rooms\\", "");
                //string[] splitName = shortfile.Split(new char[] { '_' });
                //int number = int.Parse(splitName[1]);
                string shortfile = RoomNameParser.GetShortFilename(file);
                int number = RoomNameParser.GetNumberFromFilename(shortfile);
                if ((number & pattern) == pattern)
                {
                    pool.Add(shortfile);
                }
            }
        }
        return pool;
    }
}
