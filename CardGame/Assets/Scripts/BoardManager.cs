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
    private int cardHeighOffset = 50;
    private float cardRatio = 0.8f;
    private float cardRotation = 4;
    private float cardYOffsetRotation = 4;

	// Use this for initialization
	void Start () {
        _hand = new ArrayList();	
	}
	
	// Update is called once per frame
	void Update () {
		
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

    private void manageCardsPosition()
    {
        float width = ((RectTransform)transform).rect.width;

        float move = width / 12;
        int cardNumber = _hand.Count;
        int middleCardNumber = _hand.Count / 2;

        int index = 0;
        foreach(Card card in _hand)
        {
            int offsetNumber = index - middleCardNumber;
            card.transform.localPosition = new Vector3(move * offsetNumber + cardWidth / 4
                , cardHeight*cardRatio/2 - cardHeighOffset - cardYOffsetRotation * offsetNumber * offsetNumber
                , 0.0f);
            card.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -cardRotation * offsetNumber);
            index++;
        }
    }
}
