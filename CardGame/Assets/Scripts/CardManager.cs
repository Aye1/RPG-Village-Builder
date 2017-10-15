using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class CardManager : MonoBehaviour {

    public Card emptyCard;
    private string _filename = "cards.json";
    private CardModel[] _cardsList;
    private Canvas _canvas;

    public bool areCardsLoaded = false;

	// Use this for initialization
	void Start () {
        LoadCards();
        _canvas = FindObjectOfType<Canvas>();
	}

    private void LoadCards()
    {
        string fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "Resources/" + _filename));
        if (File.Exists(fullPath))
        {
            string rawData = File.ReadAllText(fullPath);
            WrapperCard cardsArray = JsonUtility.FromJson<WrapperCard>(rawData);
            if (cardsArray != null)
            {
                _cardsList = cardsArray.cards;
                areCardsLoaded = true;
            }
        }
    }

    public int GetCardCount()
    {
        return _cardsList.Length;
    }

    public Card CreateCard(int index)
    {
        if (index >= _cardsList.Length)
        {
            Debug.LogError("Wrong card index");
            return null;
        }
        else
        {
            Card res = Instantiate(emptyCard);
            res.LoadCardModel(_cardsList[index]);
            res.transform.SetParent(_canvas.transform);
            res.transform.localPosition = Vector3.zero;
            res.transform.localScale = Vector3.one;
            return res;
        }
    }

    [Serializable]
    private class WrapperCard
    {
        public CardModel[] cards;
    }

}
