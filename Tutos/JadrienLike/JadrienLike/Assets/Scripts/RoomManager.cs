using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour {

    //public int currentRoomId;
    public int currentRoomX;
    public int currentRoomY;

    private Vector2 _nextRoomPos;

    public bool canStartChecking;

    public static readonly int RoomWidth = 18;
    public static readonly int RoomHeight = 11;
    private static readonly string RoomFolder = "Rooms/";

    private bool _init;

    private Player _player;
    private Camera _camera;
    private Camera_behaviour _camera_behaviour;
    private BoardManager _boardManager;
    private Dictionary<Vector2, Room> _rooms;

    public Dictionary<Vector2, Room> Rooms
    {
        get
        {
            return _rooms;
        }
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    public void Init()
    {
        if (!_init)
        {
            currentRoomX = 0;
            currentRoomY = 0;
            _nextRoomPos = Vector2.zero;
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
        _rooms.Add(new Vector2(currentRoomX, currentRoomY), room);
    }

    private void CheckPlayerPosition()
    {
        // Don't check position before the board is completely loaded
        if (!canStartChecking)
            return;

        int newRoomX = currentRoomX;
        int newRoomY = currentRoomY;

        bool shouldMove = false;
        Vector3 move = Vector3.zero;
        if (_player.transform.position.x > (currentRoomX+1)*RoomWidth-0.5)
        {
            Debug.Log("Changement de pièce");
            shouldMove = true;
            newRoomX++;
            move = new Vector3(1.2f, 0.0f, 0.0f);
        }
        else if(_player.transform.position.x < currentRoomX * RoomWidth + 0.5)
        {
            shouldMove = true;
            newRoomX--;
            move = new Vector3(-1.2f, 0.0f, 0.0f);
        }
        else if (_player.transform.position.y < currentRoomY * RoomHeight + 0.3)
        {
            shouldMove = true;
            newRoomY--;
            move = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if (_player.transform.position.y > (currentRoomY+1) * RoomHeight - 0.3)
        {
            shouldMove = true;
            newRoomY++;
            move = new Vector3(0.0f, 1.2f, 0.0f);
        }

        // Update the next room pos for the other methods
        _nextRoomPos.x = newRoomX;
        _nextRoomPos.y = newRoomY;

        if (shouldMove)
        {
            
            if (!_rooms.ContainsKey(_nextRoomPos))
            {
                LoadRoom(SelectNextRoom());
                //Room loadedRoom = _boardManager.LoadRoom(SelectNextRoom(), offset);
            }
            _rooms[_nextRoomPos].OnPlayerEnter();
            _rooms[new Vector2(currentRoomX, currentRoomY)].OnPlayerExit();
            MoveCamera();
            currentRoomX = newRoomX;
            currentRoomY = newRoomY;
        }
        MovePlayer(move);
    }

    /// <summary>
    /// Moves the camera so that it follows the current room
    /// </summary>
    private void MoveCamera()
    {
        Vector3 offset = new Vector3(_nextRoomPos.x * RoomWidth, _nextRoomPos.y * RoomHeight, 0.0f);
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
    public void LoadRoom(string roomName)
    {
        Vector3 offset = new Vector3(_nextRoomPos.x * RoomWidth, _nextRoomPos.y * RoomHeight, 0.0f);
        Room room = new Room(roomName);
        _rooms.Add(_nextRoomPos, room);
        _boardManager.LoadRoom(room, offset);
    }

    /// <summary>
    /// Selects which room should be loaded next, according to the current configuration
    /// </summary>
    /// <returns>The filename of the room to be loaded</returns>
    private string SelectNextRoom()
    {
        string nextRoomPath = "";
        List<string> pool = CreatePathPool(ComputePatterns());
        string selectedRoom = pool[Random.Range(0, pool.Count)];
        nextRoomPath = RoomFolder  + selectedRoom;
        return nextRoomPath;
    }

    /// <summary>
    /// Computes the positive and negative patterns for the doors/walls
    /// </summary>
    /// <returns></returns>
    private Vector2 ComputePatterns()
    {
        int positivePattern = 0;
        int negativePattern = 0;
        Vector2 leftVector = new Vector2(_nextRoomPos.x - 1, _nextRoomPos.y);
        Vector2 rightVector = new Vector2(_nextRoomPos.x + 1, _nextRoomPos.y);
        Vector2 topVector = new Vector2(_nextRoomPos.x, _nextRoomPos.y + 1);
        Vector2 bottomVector = new Vector2(_nextRoomPos.x, _nextRoomPos.y - 1);
        if (_rooms.ContainsKey(leftVector))
        {
            Room _leftRoom = _rooms[leftVector];
            if (_leftRoom.doorRightBot)
            {
                positivePattern += 128;
            }
            else
            {
                negativePattern += 128;
            }
            if (_leftRoom.doorRightTop)
            {
                positivePattern += 64;
            }
            else
            {
                negativePattern += 64;
            }
        }
        if (_rooms.ContainsKey(rightVector))
        {
            Room _rightRoom = _rooms[rightVector];
            if (_rightRoom.doorLeftBot)
            {
                positivePattern += 4;
            }
            else
            {
                negativePattern += 4;
            }
            if (_rightRoom.doorLeftTop)
            {
                positivePattern += 8;
            }
            else
            {
                negativePattern += 8;
            }
        }
        if (_rooms.ContainsKey(topVector))
        {
            Room _topRoom = _rooms[topVector];
            if (_topRoom.holeBottomLeft)
            {
                positivePattern += 32;
            }
            else
            {
                negativePattern += 32;
            }
            if (_topRoom.holeBottomRight)
            {
                positivePattern += 16;
            }
            else
            {
                negativePattern += 16;
            }
        }
        if (_rooms.ContainsKey(bottomVector))
        {
            Room _bottomRoom = _rooms[bottomVector];
            if (_bottomRoom.holeTopLeft)
            {
                positivePattern += 1;
            }
            else
            {
                negativePattern += 1;
            }
            if (_bottomRoom.holeTopRight)
            {
                positivePattern += 2;
            }
            else
            {
                negativePattern += 2;
            }
        }
        return new Vector2(positivePattern, negativePattern);
    }

    /// <summary>
    /// Creates the list of rooms which have the correct options
    /// </summary>
    /// <param name="pattern">Pattern which defines the options</param>
    /// <returns>A list of the filenames of the correct rooms</returns>
    private List<string> CreatePathPool(Vector2 patterns)
    {
        List<string> pool = new List<string>();
        foreach(string file in System.IO.Directory.GetFiles("Assets/Resources/Rooms"))
        {
            if (!file.EndsWith(".meta") && !file.EndsWith(".ini.xml"))
            {
                string shortfile = RoomNameParser.GetShortFilename(file);
                int number = RoomNameParser.GetNumberFromFilename(shortfile);
                if ((number & (int)patterns.x) == patterns.x && (((int)patterns.y & ~number) == patterns.y))
                {
                    pool.Add(shortfile);
                }
            }
        }
        if (pool.Count == 0)
        {
            Debug.Log("No room found for this configuration:" + patterns.x + " - " + patterns.y);
        }
        return pool;
    }
}
