using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelection : MonoBehaviour {
	public int selectedCard;
	public float spd;
	TurnController myTCont;
	public Vector3 hiddenCardsPos;
	public bool amHidden;

	public int currSymbolToUse;

	// Use this for initialization
	void Start () {
		selectedCard = 1;
		currSymbolToUse = 0;
		myTCont = GameObject.Find ("GameManager").GetComponent<TurnController> ();
		hiddenCardsPos = transform.position;
		amHidden = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Find ("TurnText").GetComponent<TextMeshPro> ().text = "Turn " + myTCont.currTurn;

		if (myTCont.currTurnPhase.Substring (0, 4) == "Pick") {
			amHidden = false;
		} else {
			amHidden = true;
		}

		if (currSymbolToUse == 4) {
			myTCont.currPhase++;
			currSymbolToUse = 0;
		}

		//		if (Input.GetKeyUp (KeyCode.DownArrow)) {
		//			amHidden = true;
		//		}

		if (!amHidden) {
			if (transform.position != Vector3.zero) {
				transform.position = Vector3.MoveTowards (transform.position, Vector3.zero, spd * Time.deltaTime);
			} else {
				if (Input.GetKeyUp(KeyCode.LeftArrow)){
					selectedCard--;
					if (selectedCard < 1) {
						selectedCard = 1;
					}
				}
				if (Input.GetKeyUp(KeyCode.RightArrow)){
					selectedCard++;
					if (selectedCard > 4) {
						selectedCard = 4;
					}
				}

				for (int i = 0; i < 4; i++) {
					if (transform.GetChild (i).name == "Card_" + selectedCard) {
						transform.GetChild (i).GetComponent<SpriteRenderer> ().color = Color.white;
					} else {
						transform.GetChild (i).GetComponent<SpriteRenderer> ().color = new Color (0.75f, 0.75f, 0.75f);

					}
				}

				if (Input.GetKeyUp (KeyCode.Z) && !transform.GetChild(selectedCard - 1).GetComponent<CardInfo> ().isChosen) {
					transform.GetChild(selectedCard - 1).GetComponent<CardInfo> ().selectionType = currSymbolToUse;
					transform.GetChild (selectedCard - 1).GetComponent<CardInfo> ().isChosen = true;
					currSymbolToUse++;
				}
			}
		} else {
			if (transform.position != hiddenCardsPos) {
				transform.position = Vector3.MoveTowards (transform.position, hiddenCardsPos, spd * Time.deltaTime);
			}
		}
	}
}
