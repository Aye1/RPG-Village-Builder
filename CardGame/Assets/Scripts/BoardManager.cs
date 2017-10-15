using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject playerHand;
    private ArrayList _hand;

    private const int _maxCards = 10;
    public float leftLimit = 0.0f;
    public float rightLimit = 1.0f;

    private int cardWidth = 150;
    private int cardHeight = 300;
    private int cardHeightOffset = 50;
    private float cardRatio = 0.7f;
    private float cardRotation = 4;
    private float zOffset = 1.0f;
    private float meanZOffset = -10.0f;

	// Use this for initialization
	void Start () {
        _hand = new ArrayList();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetCardsPosition()
    {
        resetCardsPosition();
    }

    public bool canAddCardToHand()
    {
        return _hand.Count < _maxCards;
    }

    public void addCardToHand(Card card)
    {
        _hand.Add(card);
        card.transform.SetParent(playerHand.transform);
        card.transform.localScale = new Vector3(cardRatio, cardRatio, 1.0f);
        manageCardsPosition();
    }

    /*public void addCardToHandWithMoveLeft(Card card)
    {
        moveCardsToLeft();
        _hand.Add(card);
        card.transform.SetParent(playerHand.transform);
        card.transform.localScale = new Vector3(cardRatio, cardRatio, 1.0f);
        moveNewCard(card);
    }*/
    
    private void resetCardsPosition()
    {
        float width = ((RectTransform)transform).rect.width;

        float move = width / 10;
        int cardNumber = _hand.Count;
        int middleCardNumber = _hand.Count / 2;

        int index = 0;
        foreach (Card card in _hand)
        {
            int offsetNumber = index - middleCardNumber;
            card.transform.localPosition = new Vector3(move * offsetNumber + cardWidth / 3
                , cardHeight * cardRatio / 2 - cardHeightOffset
                , -zOffset * offsetNumber + meanZOffset);
            index++;
        }
    }

    private void manageCardsPosition()
    {
        resetCardsPosition();
    }

    /*private void moveCardsToLeft()
    {
        foreach(Card card in _hand)
        {
            card.transform.localPosition += new Vector3(-50.0f, 0.0f, -0.5f);
        }
    }

    private void moveNewCard(Card card)
    {
        int cardNumber = _hand.Count;
        int index = cardNumber - 1;
        int middleCardNumber = _hand.Count / 2;
        int offsetNumber = index - middleCardNumber;
        float move = 10.0f;
        card.transform.localPosition = new Vector3(move * offsetNumber + cardWidth / 3
            , cardHeight * cardRatio / 2 - cardHeightOffset
            , -zOffset * offsetNumber + meanZOffset);
    }*/
}
