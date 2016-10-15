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

    public bool largeMap = false;

	// Use this for initialization
	void Start () {
        _roomManager = FindObjectOfType<RoomManager>();
        _roomImages = new Dictionary<Vector2, Image>();
        RectTransform rectTransform = (RectTransform) transform;
        rectTransform.sizeDelta = new Vector2(_sizeRoomX * _mapSize, _sizeRoomY * _mapSize); 
	}
	
	// Update is called once per frame
	void Update () {
        Dictionary<Vector2, Room> rooms = _roomManager.Rooms;
        Vector2 offset = new Vector2(_roomManager.currentRoomX, _roomManager.currentRoomY);
        foreach (KeyValuePair<Vector2, Room> pair in rooms)
        {
            Vector2 pos = pair.Key;
            if(!_roomImages.ContainsKey(pos))
            {
                Image img = CreateImageAtPos(pos);
                _roomImages.Add(pos, img);
            }
        }
        foreach (KeyValuePair<Vector2, Image> pair in _roomImages)
        {
            Image img = pair.Value;
            Vector3 offsetPos = pair.Key - offset;
            img.transform.localPosition = new Vector3(offsetPos.x * _sizeRoomX, offsetPos.y * _sizeRoomY, 0.0f);
            if(offsetPos.x < -_halfMapSize || offsetPos.x > _halfMapSize || offsetPos.y < -_halfMapSize || offsetPos.y > _halfMapSize)
            {
                img.enabled = false;
            }
            else
            {
                img.enabled = true;
            }
        }
	}

    private Image CreateImageAtPos(Vector2 pos)
    {
        Image resImage = null;
        resImage = (Image)Instantiate(roomImage, pos, Quaternion.identity);
        resImage.transform.SetParent(transform);
        resImage.transform.localScale = new Vector3(_imageScale, _imageScale, _imageScale);
        resImage.transform.localPosition = new Vector3(pos.x * _sizeRoomX, pos.y * _sizeRoomY, 0.0f);
        return resImage;
    }
}
