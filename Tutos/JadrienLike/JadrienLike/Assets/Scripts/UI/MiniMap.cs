using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {

    public Image roomImage;
    private RoomManager _roomManager;
    private Dictionary<Vector2, Image> _roomImages;
    private const float _imageScale = 0.2f;
    private const float _imageInitialPNGRatio = 10.0f;
    private float _sizeRoomX = RoomManager.RoomWidth * _imageScale * _imageInitialPNGRatio;
    private float _sizeRoomY = RoomManager.RoomHeight * _imageScale * _imageInitialPNGRatio;
    // Number of rooms which can be displayed in each dimension
    private const int _mapSize = 7;
    private const float _halfMapSize = _mapSize / 2.0f;

    private const float flatPadding = 100.0f;

    private Vector3 _initialPosition;
    private Vector3 _fullMapSize;

    private Camera _camera;

    public bool fullMap = false;

	// Use this for initialization
	void Start () {
        _roomManager = FindObjectOfType<RoomManager>();
        _roomImages = new Dictionary<Vector2, Image>();
        _camera = FindObjectOfType<Camera>();
        _initialPosition = new Vector3(350.0f, 180.0f, 0.0f);

        Vector3 minResolution = Camera_behaviour.minResolution;
        _fullMapSize = new Vector3(minResolution.x - flatPadding, minResolution.y - flatPadding, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        SetSize();
        UpdateMissingRooms();
        DrawRooms();
	}

    private void SetSize()
    {
        RectTransform rectTransform = (RectTransform)transform;
        if (fullMap)
        {
            rectTransform.sizeDelta = _fullMapSize;
            rectTransform.localPosition = Vector3.zero;
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(_sizeRoomX * _mapSize, _sizeRoomY * _mapSize);
            rectTransform.localPosition = _initialPosition;
        }
    }

    private void UpdateMissingRooms()
    {
        Dictionary<Vector2, Room> rooms = _roomManager.Rooms;
        foreach (KeyValuePair<Vector2, Room> pair in rooms)
        {
            Vector2 pos = pair.Key;
            if (!_roomImages.ContainsKey(pos))
            {
                Image img = CreateImageAtPos(pos);
                _roomImages.Add(pos, img);
            }
        }
    }

    private void DrawRooms()
    {
        Vector2 offset = new Vector2(_roomManager.currentRoomX, _roomManager.currentRoomY);
        Vector2 roomSize = GetRoomSize();
        float ratio = GetImageScale(roomSize);
        foreach (KeyValuePair<Vector2, Image> pair in _roomImages)
        {
            Image img = pair.Value;
            img.transform.localScale = new Vector2(ratio, ratio);
            Vector3 offsetPos = pair.Key - offset;
            img.transform.localPosition = new Vector3(offsetPos.x * roomSize.x, offsetPos.y * roomSize.y, 0.0f);
            if (!fullMap 
                && (offsetPos.x < -_halfMapSize 
                || offsetPos.x > _halfMapSize 
                || offsetPos.y < -_halfMapSize 
                || offsetPos.y > _halfMapSize))
            {
                img.enabled = false;
            }
            else
            {
                img.enabled = true;
            }
        }
    }

    private float GetImageScale(Vector2 size)
    {
        float ratio = size.x / _sizeRoomX * _imageScale ;
        return ratio;
    }

    private Image CreateImageAtPos(Vector2 pos)
    {
        Image resImage = null;
        resImage = (Image)Instantiate(roomImage, pos, Quaternion.identity);
        resImage.transform.SetParent(transform);
        resImage.transform.localScale = new Vector3(_imageScale, _imageScale, _imageScale);

        Vector2 roomSize = GetRoomSize();
        resImage.transform.localPosition = new Vector3(pos.x * roomSize.x, pos.y * roomSize.y, 0.0f);
        return resImage;
    }

    private Vector2 GetRoomSize()
    {
        Vector2 res = Vector2.zero;
        if (fullMap)
        {
            float size = GetMapSize();
            res.x = Mathf.Min(_fullMapSize.x / size / 2, _sizeRoomX);
            res.y = Mathf.Min(_fullMapSize.y / size / 2, _sizeRoomY);
        }
        else
        {
            res.x = _sizeRoomX;
            res.y = _sizeRoomY;
        }
        return res;
    }

    private float GetMapSize()
    {
        float minX = 0;
        float maxX = 0;
        float minY = 0;
        float maxY = 0;
        foreach (KeyValuePair<Vector2, Image> pair in _roomImages)
        {
            Vector2 pos = pair.Key;
            minX = (pos.x < minX) ? pos.x : minX;
            maxX = (pos.x > maxX) ? pos.x : maxX;
            minY = (pos.y < minY) ? pos.y : minY;
            maxY = (pos.y > maxY) ? pos.y : maxY;
        }
        float lengthX = maxX - minX;
        float lengthY = maxY - minY;
        return Mathf.Max(lengthX, lengthY) + 1;
    }
}
