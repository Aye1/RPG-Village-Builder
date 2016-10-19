using System.Collections.Generic;

public class DynamicMapObject {

    private MapObjectType _objectType;
    public Dictionary<string, string> properties;
    public int x;
    public int y;

    public MapObjectType ObjectType
    {
        get
        {
            return _objectType;
        }
        set
        {
            if (value != _objectType)
            {
                _objectType = value;
            }
        }
    }

    public DynamicMapObject(string type)
    {
        if (type.Equals("Door"))
        {
            _objectType = MapObjectType.Door;
        }
        properties = new Dictionary<string, string>();
    }
}
