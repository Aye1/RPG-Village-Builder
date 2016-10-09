using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

    public int _currentRoomId;
    public int _currentRoomX;
    public int _currentRoomY;

    private const int _roomWidth = 18;
    private const int _roomHeight = 10; 

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
    }

    // Update is called once per frame
    void Update() {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        bool shouldMove = false;
        Vector3 move = Vector3.zero;
        if (_player.transform.position.x > (_currentRoomX+1)*_roomWidth-0.5)
        {
            Debug.Log("Changement de pièce");
            shouldMove = true;
            _currentRoomX++;
            move = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else if(_player.transform.position.x < _currentRoomX * _roomWidth + 0.5)
        {
            shouldMove = true;
            _currentRoomX--;
            move = new Vector3(-1.0f, 0.0f, 0.0f);
        }

        if (shouldMove)
        {
            Vector3 offset = new Vector3(_currentRoomX * _roomWidth, _currentRoomY * _roomHeight, 0.0f);
            if (!_rooms.ContainsKey(new Vector2(_currentRoomX, _currentRoomY)))
            {
                _boardManager.LoadRoom("simple-18-11", offset);
                _rooms.Add(new Vector2(_currentRoomX, _currentRoomY), true);
            }
            MoveCamera();
        }
        MovePlayer(move);
    }

    private void MoveCamera()
    {
        Vector3 offset = new Vector3(_currentRoomX * _roomWidth, _currentRoomY * _roomHeight, 0.0f);
        _camera_behaviour.Offset = offset;
    }

    private void MovePlayer(Vector3 move)
    {
        _player.transform.position += move;
    }
}
