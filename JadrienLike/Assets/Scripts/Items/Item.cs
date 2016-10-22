using UnityEngine;

public abstract class Item : MonoBehaviour {
    protected Player _player;

    public abstract void Init();
    public abstract void OnPlayerTouches();


    public void Flip()
    {
        Vector3 itemScale = transform.localScale;
        itemScale.x *= -1;
        transform.localScale = itemScale;
    }

    void Start()
    {
        _player = FindObjectOfType<Player>();
        Init();  
    }
}
