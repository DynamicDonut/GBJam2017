using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour {
	public int posX, posY, moveCounter, currMoves;
	public Sprite[] mechOptions;
	GenerateGrid myGridScript;
	TurnController myTurnCont;
	bool ignoreMove;
	public bool p1MechTeam;

	// Use this for initialization
	void Start () {
		//posX = posY = 0;
		moveCounter = Random.Range (3, 6);
		currMoves = moveCounter;
		ignoreMove = false;
		p1MechTeam = true;
		myGridScript = GameObject.Find ("GridGen").GetComponent<GenerateGrid> ();
		myTurnCont = GameObject.Find ("GameManager").GetComponent<TurnController> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyUp (KeyCode.Z)) {
//			AttackCloseRange (posX, posY);
//		}

		if (this.name == "B1" || this.name == "B2") {
			p1MechTeam = false;
		}

		if (myTurnCont.myCards.position == myTurnCont.myCards.GetComponent<CardSelection> ().hiddenCardsPos) {
			if (myTurnCont.currPhase == 1 && this.name == "A1") {
				myTurnCont.myUI.Find ("P1 Info").GetChild (3).GetComponent<TextMeshPro> ().text = currMoves.ToString();
				TimeToMove ("P1");
			}
			if (myTurnCont.currPhase == 2 && this.name == "A2") {
				myTurnCont.myUI.Find ("P1 Info").GetChild (3).GetComponent<TextMeshPro> ().text = currMoves.ToString();
				TimeToMove ("P1");
			}
			if (myTurnCont.currPhase == 4 && this.name == "B1") {
				myTurnCont.myUI.Find ("P2 Info").GetChild (3).GetComponent<TextMeshPro> ().text = currMoves.ToString();
				TimeToMove ("P2");
			}
			if (myTurnCont.currPhase == 5 && this.name == "B2") {
				myTurnCont.myUI.Find ("P2 Info").GetChild (3).GetComponent<TextMeshPro> ().text = currMoves.ToString();
				TimeToMove ("P2");
			}

//			if (myTurnCont.currPhase == 6) {
//				if (this.name == "A1") {
//					AttackCloseRange (posX, posY, "B");
//				}
//				if (this.name == "A2") {
//					AttackCloseRange (posX, posY, "B");
//				} 
//				if (this.name == "B1") {
//					AttackCloseRange (posX, posY, "A");
//				}
//				if (this.name == "B2") {
//					AttackCloseRange (posX, posY, "A");
//				}
//			}
		}
	}

	void TimeToMove(string pS){
		if (currMoves != 0) {
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				if (posX < myGridScript.gridSize-1) {
					posX++;
					MovementCheck ("right", pS);
				}
			}
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				if (posX > 0) {
					posX--;
					MovementCheck ("left", pS);
				}
			}
			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				if (posY < myGridScript.gridSize-1) {
					posY++;
					MovementCheck ("up", pS);
				}
			}
			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				if (posY > 0) {
					posY--;
					MovementCheck ("down", pS);
				}
			}
		}
	}

	void MovementCheck(string dir, string playerSide){
		GameObject[] mechSpots = GameObject.FindGameObjectsWithTag ("Mech");
		string areaTest = "(" + posX + "," + posY + ")";
		foreach (string building in myGridScript.buildingAreas) {
			if (areaTest == building) {
				ignoreMove = true;
			}
		}

		for (int i = 0; i < 4; i++){
			if (mechSpots [i].name != this.name) {
				string otherMech = "(" + mechSpots [i].GetComponent<PlayerMovement> ().posX + "," + mechSpots [i].GetComponent<PlayerMovement> ().posY + ")";
				if (areaTest == otherMech) {
					ignoreMove = true;
				}
			}
		}

		if (!ignoreMove) {
			Vector3 newPos = GameObject.Find ("Cell (" + posX + "," + posY + ")").transform.position;
			currMoves--;
			transform.position = newPos;

			myTurnCont.myUI.Find (playerSide + " Info").GetChild (3).GetComponent<TextMeshPro> ().text = currMoves.ToString();
			if (currMoves == 0) {
				myTurnCont.currPhase++;
				currMoves = moveCounter;
			}
		} else {
			if (dir == "up") {
				posY--;
			} else if (dir == "down") {
				posY++;
			} else if (dir == "left") {
				posX++;
			} else if (dir == "right") {
				posX--;
			} 
			ignoreMove = false;
		}
	}

	public void TeamSet(bool isP1, int myMech, Vector2 myPos, string myName){
		if (!isP1) {
			this.GetComponent<SpriteRenderer> ().color = new Color (0.75f, 0.75f, 0.75f);
			this.GetComponent<SpriteRenderer> ().flipX = true;
		}
		posX = (int) myPos.x; posY = (int) myPos.y;
		this.GetComponent<SpriteRenderer> ().sprite = mechOptions [myMech];
		this.name = myName;
	}

	public void AttackCloseRange(int myX, int myY){
//		GameObject.Find ("Cell (" + (myX - 1) + "," + myY + ")").GetComponent<SpriteRenderer> ().color = Color.black;
//		GameObject.Find ("Cell (" + (myX + 1) + "," + myY + ")").GetComponent<SpriteRenderer> ().color = Color.black;
//		GameObject.Find ("Cell (" + myX + "," + (myY - 1) + ")").GetComponent<SpriteRenderer> ().color = Color.black;
//		GameObject.Find ("Cell (" + myX + "," + (myY + 1) + ")").GetComponent<SpriteRenderer> ().color = Color.black;

		GameObject[] mechSpots = GameObject.FindGameObjectsWithTag ("Mech");
		for(int i = 0; i < 4; i++){
			if (mechSpots [i].GetComponent<PlayerMovement> ().posX == posX) {
				if (mechSpots [i].GetComponent<PlayerMovement> ().posY == posY + 1 || mechSpots [i].GetComponent<PlayerMovement> ().posY == posY - 1) {
					if (!p1MechTeam) {
						myTurnCont.p1_health--;
					} else {
						myTurnCont.p2_health--;
					}

					Debug.Log ("Attack Hit! " + mechSpots [i].name + " was hit with 1 DMG!");
				}
			}

			if (mechSpots [i].GetComponent<PlayerMovement> ().posY == posY) {
				if (mechSpots [i].GetComponent<PlayerMovement> ().posX == posX + 1 || mechSpots [i].GetComponent<PlayerMovement> ().posX == posX - 1) {
					if (!p1MechTeam) {
						myTurnCont.p1_health--;
					} else {
						myTurnCont.p2_health--;
					}

					Debug.Log ("Attack Hit! " + mechSpots [i].name + " was hit with 1 DMG!");
				}
			}
		}
	}
}
