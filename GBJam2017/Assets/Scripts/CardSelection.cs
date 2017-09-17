using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelection : MonoBehaviour {
	public int selectedCard;
	public float spd;
	TurnController myTCont; GMScript myGM;
	public Vector3 hiddenCardsPos;
	public bool amHidden, cardsChosen;

	public int currSymbolToUse;

	// Use this for initialization
	void Start () {
		selectedCard = 1;
		currSymbolToUse = 0;
        myTCont = GameObject.Find("GameManager").GetComponent<TurnController>();
        myGM = GameObject.Find("GameManager").GetComponent<GMScript>();
        hiddenCardsPos = transform.position;
		amHidden = true;
        cardsChosen = false;
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

                if (!cardsChosen & transform.position == Vector3.zero)
                {
                    AddCardInfo(myTCont.currPhase);
                    cardsChosen = true;
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
                    if (myTCont.currPhase == 0)
                    {
                        if (currSymbolToUse == 0)
                        {
                            GameObject.Find("A1").GetComponent<PlayerMovement>().nextMove = transform.GetChild(selectedCard - 1).GetComponent<CardInfo>().functionToRun;
                        }
                        if (currSymbolToUse == 1)
                        {
                            GameObject.Find("A2").GetComponent<PlayerMovement>().nextMove = transform.GetChild(selectedCard - 1).GetComponent<CardInfo>().functionToRun;
                        }
                    } else if (myTCont.currPhase == 3)
                    {
                        if (currSymbolToUse == 0)
                        {
                            GameObject.Find("B1").GetComponent<PlayerMovement>().nextMove = transform.GetChild(selectedCard - 1).GetComponent<CardInfo>().functionToRun;
                        }
                        if (currSymbolToUse == 1)
                        {
                            GameObject.Find("B2").GetComponent<PlayerMovement>().nextMove = transform.GetChild(selectedCard - 1).GetComponent<CardInfo>().functionToRun;
                        }
                    }
					transform.GetChild (selectedCard - 1).GetComponent<CardInfo> ().isChosen = true;
					currSymbolToUse++;
				}
			}
		} else {
			if (transform.position != hiddenCardsPos) {
				transform.position = Vector3.MoveTowards (transform.position, hiddenCardsPos, spd * Time.deltaTime);
			}
            if (transform.position == hiddenCardsPos)
            {
                cardsChosen = false;
            }
        }
	}

   public void AddCardInfo(int currPhase)
    {
        if (currPhase == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                int myCard = Random.Range(0, myGM.p1CardDeck.Count);
                Debug.Log(myCard);
                string stringToRemove = myGM.p1CardDeck[myCard];
                transform.GetChild(i).GetComponent<CardInfo>().FillCardInfo(myGM.p1CardDeck[myCard]);
                if (i == 2)
                {
                    myGM.p1CardDeck.RemoveAt(myCard);
                }
            }
        }

        if (currPhase == 3)
        {
            for (int i = 0; i < 4; i++)
            {
                int myCard = Random.Range(0, myGM.p2CardDeck.Count);
                string stringToRemove = myGM.p2CardDeck[myCard];
                transform.GetChild(i).GetComponent<CardInfo>().FillCardInfo(myGM.p2CardDeck[myCard]);
                if (i == 2)
                {
                    myGM.p2CardDeck.RemoveAt(myCard);
                }
            }
        }
    }
}
