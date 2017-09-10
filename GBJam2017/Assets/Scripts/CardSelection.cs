using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection : MonoBehaviour {
	public int selectedCard;
	public float spd;
	TurnController myTCont;
	Vector3 hiddenCardsPos;
	public bool amHidden;

	// Use this for initialization
	void Start () {
		selectedCard = 1;
		myTCont = GameObject.Find ("GameManager").GetComponent<TurnController> ();
		hiddenCardsPos = transform.position;
		amHidden = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (myTCont.currTurnPhase.Substring (0, 4) == "Pick") {
			amHidden = false;
		} else {
			amHidden = true;
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
			}
		} else {
			if (transform.position != hiddenCardsPos) {
				transform.position = Vector3.MoveTowards (transform.position, hiddenCardsPos, spd * Time.deltaTime);
			}
		}
	}
}
