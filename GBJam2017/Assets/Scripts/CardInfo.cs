using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardInfo : MonoBehaviour {
	public string cardName, styleSource, myDeck, functionToRun;
	public bool isChosen = false;
	public Sprite cardLogo;
	public int selectionType; // 0 = mech 1, 1 = mech 2, 2 = hold card, 3 = discard card, 4 = none

	public Sprite mySprite;
	public Sprite[] symbolTypes; // 0 = mech 1, 1 = mech 2, 2 = hold card, 3 = discard card, 4 = none
	Transform mySymbol, myText;

	// Use this for initialization
	void Start () {
		selectionType = 4;
		mySprite = GetComponent<SpriteRenderer> ().sprite;
        mySymbol = transform.GetChild(0);
        myText = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update () {
		mySymbol.GetComponent<SpriteRenderer>().sprite = symbolTypes [selectionType];
        myText.GetComponent<TextMeshPro>().text = cardName;


		if (transform.parent.GetComponent<CardSelection>().amHidden && transform.parent.position == transform.parent.GetComponent<CardSelection>().hiddenCardsPos){
			mySymbol.GetComponent<SpriteRenderer> ().sprite = null;
			selectionType = 4;
			isChosen = false;
		}
	}

    public void FillCardInfo(string s)
    {
        functionToRun = s;
        cardName = s.Substring(2, s.Length - 2);
    }
}
