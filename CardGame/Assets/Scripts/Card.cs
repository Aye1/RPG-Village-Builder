using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour {
    [Header("Values")]
    public int attack;
    public int hp;
    public int cost;
    public int influence;
    public string infosPath;
    public string cardName;
    public string type;

    private string illusName;

    [Header("Textfields")]
    public Text atkText;
    public Text hpText;
    public Text infText;
    public Text costText;
    public Text nameText;

    [Header("Other")]
    public GameObject illustration;

    [Header("Debug")]
    public bool isMouseOver = false;

    private const string resourcesFolder = "Resources";
    private const string illusFolder = "Illustrations";

    public string IllustrationName
    {
        get { return illusName; }
        set
        {
            if (value != illusName)
            {
                illusName = value;
                UpdateIllustration();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTextFields();
        UpdateIllustration();
	}

    public void LoadCardModel(CardModel model)
    {
        attack = model.attack;
        hp = model.hp;
        influence = model.influence;
        cost = model.cost;
        cardName = model.cardName;
        IllustrationName = model.illusName;
    }

    void UpdateTextFields()
    {
        atkText.text = attack.ToString();
        hpText.text = hp.ToString();
        infText.text = influence.ToString();
        costText.text = cost.ToString();
        nameText.text = cardName.ToString();
    }

    private void UpdateIllustration()
    {
        if (!string.IsNullOrEmpty(IllustrationName))
        {
            string resourcePath = Path.Combine(illusFolder, illusName);
            string fullResourcePath = Path.Combine(resourcesFolder, resourcePath);
            string completePath = Path.GetFullPath(Path.Combine(Application.dataPath, fullResourcePath));
            if (File.Exists(completePath))
            {
                string fileWithoutExtension = Path.GetFileNameWithoutExtension(resourcePath);
                string simplePath = "Illustrations/" + fileWithoutExtension;
                Sprite loadedSprite = Resources.Load<Sprite>(simplePath);

                SpriteRenderer renderer = illustration.GetComponentInChildren<SpriteRenderer>();
                renderer.sprite = loadedSprite;
            }
        }
    }
}
