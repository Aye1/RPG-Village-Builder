using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour {

    public int _currentRoomId;
    public int _currentRoomX;
    public int _currentRoomY;

    public bool canStartChecking;

    public static readonly int RoomWidth = 18;
    public static readonly int RoomHeight = 10;
    private static readonly string RoomFolder = "Rooms/";

    private bool _init;

    private Player _player;
    private Camera _camera;
    private Camera_behaviour _camera_behaviour;
    private BoardManager _boardManager;
    private Dictionary<Vector2, Room> _rooms;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    public void Init()
    {
        if (!_init)
        {
            _currentRoomX = 0;
            _currentRoomY = 0;
            _player = FindObjectOfType<Player>();
            _camera = FindObjectOfType<Camera>();
            _camera_behaviour = _camera.GetComponentInChildren<Camera_behaviour>();
            _boardManager = FindObjectOfType<GameController>().boardManager;
            _rooms = new Dictionary<Vector2, Room>();
            _init = true;
        }
    }

    // Update is called once per frame
    void Update() {
        CheckPlayerPosition();
    }

    public void AddFirstRoom(Room room)
    {
        // Add the first room to the dictionary of created rooms
        _rooms.Add(new Vector2(_currentRoomX, _currentRoomY), room);
    }

    private void CheckPlayerPosition()
    {
        // Don't check position before the board is completely loaded
        if (!canStartChecking)
            return;

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
                LoadRoom(SelectNextRoom(), offset);
                //Room loadedRoom = _boardManager.LoadRoom(SelectNextRoom(), offset);
            }
            MoveCamera();
        }
        MovePlayer(move);
    }

    /// <summary>
    /// Moves the camera so that it follows the current room
    /// </summary>
    private void MoveCamera()
    {
        Vector3 offset = new Vector3(_currentRoomX * RoomWidth, _currentRoomY * RoomHeight, 0.0f);
        _camera_behaviour.Offset = offset;
    }

    /// <summary>
    /// Moves the player so that it does not trigger many times
    /// </summary>
    /// <param name="move"></param>
    private void MovePlayer(Vector3 move)
    {
        _player.transform.position += move;
    }

    /// <summary>
    /// Creates a new room and instantiates it
    /// </summary>
    /// <param name="roomName"></param>
    /// <param name="offset"></param>
    public void LoadRoom(string roomName, Vector3 offset)
    {
        Room room = new Room(roomName);
        _rooms.Add(new Vector2(_currentRoomX, _currentRoomY), room);
        _boardManager.LoadRoom(room, offset);
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
            if (!file.EndsWith(".meta") && !file.EndsWith(".ini.xml"))
            {
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
