using UnityEngine;

public class DropManager : MonoBehaviour {

    private static DropManager _instance;
    public LivingCollectible sphere;

    public static DropManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DropManager();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    public void CreateSphere(Vector3 pos)
    {
        if (sphere != null)
            Instantiate(sphere, pos, Quaternion.identity);
    }
}
