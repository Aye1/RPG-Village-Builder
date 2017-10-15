using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private CardManager _cardManager;
    private BoardManager _boardManager;

	// Use this for initialization
	void Start () {
        _cardManager = FindObjectOfType<CardManager>();
        _boardManager = FindObjectOfType<BoardManager>();
        StartCoroutine(InitCards());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddZeroCard()
    {
        if (_boardManager.canAddCardToHand())
        {
            _boardManager.addCardToHand(_cardManager.CreateCard(0));
        }
    }

    public void AddRandomCard()
    {
        if (_boardManager.canAddCardToHand())
        {
            int index = Random.Range(0, _cardManager.GetCardCount()-1);
            _boardManager.addCardToHand(_cardManager.CreateCard(index));
        }
    }

    private IEnumerator InitCards()
    {
        if (_cardManager != null)
        {
            while (!_cardManager.areCardsLoaded)
            {
                yield return null;
            }
            AddRandomCard();
        }
    }
}
