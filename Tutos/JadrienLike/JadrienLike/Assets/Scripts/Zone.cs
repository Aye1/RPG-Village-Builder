using UnityEngine;
using System.Collections;

public class Zone {

    #region Tiles
    public ArrayList topSprites;
    public ArrayList bottomSprites;
    public ArrayList leftSprites;
    public ArrayList rightSprites;
    public ArrayList fullSprites;
    public ArrayList cornerblSprites;
    public ArrayList cornerbrSprites;
    public ArrayList cornertlSprites;
    public ArrayList cornertrSprites;
    public ArrayList cliffblSprites;
    public ArrayList cliffbrSprites;
    public ArrayList clifftlSprites;
    public ArrayList clifftrSprites;
    public ArrayList backgroundSprites;
    public ArrayList exitSprites;
    #endregion

    #region Private fields
    private int _id;
    #endregion

    #region Accessors
    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }
    #endregion

    private void InitArrayLists()
    {
        topSprites = new ArrayList();
        bottomSprites = new ArrayList();
        leftSprites = new ArrayList();
        rightSprites = new ArrayList();
        fullSprites = new ArrayList();
        cornerblSprites = new ArrayList();
        cornerbrSprites = new ArrayList();
        cornertlSprites = new ArrayList();
        cornertrSprites = new ArrayList();
        cliffblSprites = new ArrayList();
        cliffbrSprites = new ArrayList();
        clifftlSprites = new ArrayList();
        clifftrSprites = new ArrayList();
        backgroundSprites = new ArrayList();
        exitSprites = new ArrayList();
    }

    /// <summary>
    /// Loads and parses the different tiles of a spritesheet.
    /// </summary>
    /// <param name="zoneId">Zone identifier.</param>
    /// <param name="path">Path of the complete spritesheet in the Ressources folder</param>
    public Zone(int zoneId, string path)
    {
        InitArrayLists();
        _id = zoneId;
        Object[] sprites = Resources.LoadAll(path);
        Debug.Log("Spritesheet loaded - size: " + sprites.Length);
        foreach (Object currentObj in sprites)
        {
            if (currentObj is Sprite)
            {
                Sprite sprite = currentObj as Sprite;      
                if (sprite.name.StartsWith("background"))
                {
                    backgroundSprites.Add(sprite);
                } 
                else if (sprite.name.StartsWith("bottom"))
                {
                    bottomSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("cliff_bl"))
                {
                    cliffblSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("cliff_br"))
                {
                    cliffbrSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("cliff_tl"))
                {
                    clifftlSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("cliff_tr"))
                {
                    clifftrSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("corner_bl"))
                {
                    cornerblSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("corner_br"))
                {
                    cornerbrSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("corner_tl"))
                {
                    cornertlSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("corner_tr"))
                {
                    cornertrSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("exit"))
                {
                    exitSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("full"))
                {
                    fullSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("left"))
                {
                    leftSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("player_start"))
                {
                    // Do nothing, we don't care
                }
                else if (sprite.name.StartsWith("right"))
                {
                    rightSprites.Add(sprite);
                }
                else if (sprite.name.StartsWith("top"))
                {
                    topSprites.Add(sprite);
                } 
                else
                {
                    // Do nothing, we don't know you, tile!
                }
            }
        }
        Debug.Log("Zone " + zoneId + " loaded.");
    }

    public Sprite GetBackgroundSprite()
    {
        return backgroundSprites[Random.Range(0, backgroundSprites.Count)] as Sprite;
    }

    public Sprite GetTopSprite()
    {
        return topSprites[Random.Range(0, topSprites.Count)] as Sprite;
    }

    public Sprite GetBottomSprite()
    {
        return bottomSprites[Random.Range(0, bottomSprites.Count)] as Sprite;
    }

    public Sprite GetLeftSprite()
    {
        return leftSprites[Random.Range(0, leftSprites.Count)] as Sprite;
    }

    public Sprite GetRightSprite()
    {
        return rightSprites[Random.Range(0, rightSprites.Count)] as Sprite;
    }

    public Sprite GetCornerBLSprite()
    {
        return cornerblSprites[Random.Range(0, cornerblSprites.Count)] as Sprite;
    }

    public Sprite GetCornerBRSprite()
    {
        return cornerbrSprites[Random.Range(0, cornerbrSprites.Count)] as Sprite;
    }

    public Sprite GetCornerTLSprite()
    {
        return cornertlSprites[Random.Range(0, cornertlSprites.Count)] as Sprite;
    }

    public Sprite GetCornerTRSprite()
    {
        return cornertrSprites[Random.Range(0, cornertrSprites.Count)] as Sprite;
    }

    public Sprite GetCliffBLSprite()
    {
        return cliffblSprites[Random.Range(0, cliffblSprites.Count)] as Sprite;
    }

    public Sprite GetCliffBRSprite()
    {
        return cliffbrSprites[Random.Range(0, cliffbrSprites.Count)] as Sprite;
    }
    
    public Sprite GetCliffTLSprite()
    {
        return clifftlSprites[Random.Range(0, clifftlSprites.Count)] as Sprite;
    }
    
    public Sprite GetCliffTRSprite()
    {
        return clifftrSprites[Random.Range(0, clifftrSprites.Count)] as Sprite;
    }

    public Sprite GetFullSprite()
    {
        return fullSprites[Random.Range(0, fullSprites.Count)] as Sprite;
    }

    public Sprite GetExitSprite()
    {
        return exitSprites[Random.Range(0, exitSprites.Count)] as Sprite;
    }

}
