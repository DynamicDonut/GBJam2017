﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour {
	public int posX, posY, moveCounter, currMoves, myDMG;
	public Sprite[] mechOptions;
	GenerateGrid myGridScript;
	TurnController myTurnCont;
	bool ignoreMove;
    public string nextMove;
	public bool p1MechTeam, dmgUp, dmgMax, dodge;

	// Use this for initialization
	void Start () {
        //posX = posY = 0;
        myDMG = 1;
		moveCounter = Random.Range (3, 6);
		currMoves = moveCounter;
		ignoreMove = dmgUp = dmgMax = dodge = false;
		p1MechTeam = true;
		myGridScript = GameObject.Find ("GridGen").GetComponent<GenerateGrid> ();
		myTurnCont = GameObject.Find ("GameManager").GetComponent<TurnController> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyUp (KeyCode.Z)) {
//			AttackCloseRange (posX, posY);
//		}

        if (dmgUp)
        {
            myDMG = 2;
        } else if (dmgMax)
        {
            myDMG = 5;
        }
        else
        {
            myDMG = 1;
        }

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

    public void UseMove(string s)
    {
        if (s.Substring(0,1) == "0")
        {
            ShortRange();
        }
        if (s.Substring(0, 1) == "1")
        {
            Diagonal();
        }
        if (s.Substring(0, 1) == "2")
        {
            DamageUp();
        }
        if (s.Substring(0, 1) == "3")
        {
            DamageMax();
        }
        if (s.Substring(0, 1) == "4")
        {
            HealDamage();
        }
        if (s.Substring(0, 1) == "5")
        {
            DodgeMove();
        }
        if (s.Substring(0, 1) == "6")
        {
            Grenade();
        }
    }

	public void ShortRange(){
//		GameObject.Find ("Cell (" + (myX - 1) + "," + myY + ")").GetComponent<SpriteRenderer> ().color = Color.black;
//		GameObject.Find ("Cell (" + (myX + 1) + "," + myY + ")").GetComponent<SpriteRenderer> ().color = Color.black;
//		GameObject.Find ("Cell (" + myX + "," + (myY - 1) + ")").GetComponent<SpriteRenderer> ().color = Color.black;
//		GameObject.Find ("Cell (" + myX + "," + (myY + 1) + ")").GetComponent<SpriteRenderer> ().color = Color.black;

		GameObject[] mechSpots = GameObject.FindGameObjectsWithTag ("Mech");
		for(int i = 0; i < 4; i++){
			if (mechSpots [i].GetComponent<PlayerMovement> ().posX == posX) {
				if (mechSpots [i].GetComponent<PlayerMovement> ().posY == posY + 1 || mechSpots [i].GetComponent<PlayerMovement> ().posY == posY - 1) {
                    if (!mechSpots[i].GetComponent<PlayerMovement>().dodge)
                    {
                        if (!p1MechTeam)
                        {
                            myTurnCont.p1_health-=myDMG;
                        }
                        else {
                            myTurnCont.p2_health -= myDMG;
                        }
                    }

					Debug.Log ("Attack Hit! " + mechSpots [i].name + " was hit with 1 DMG!");
				}
			}

			if (mechSpots [i].GetComponent<PlayerMovement> ().posY == posY) {
				if (mechSpots [i].GetComponent<PlayerMovement> ().posX == posX + 1 || mechSpots [i].GetComponent<PlayerMovement> ().posX == posX - 1) {
                    if (!mechSpots[i].GetComponent<PlayerMovement>().dodge)
                    {
                        if (!p1MechTeam)
                        {
                            myTurnCont.p1_health -= myDMG;
                        }
                        else {
                            myTurnCont.p2_health -= myDMG;
                        }
                    }

					Debug.Log ("Attack Hit! " + mechSpots [i].name + " was hit with 1 DMG!");
				}
			}
		}

        if (dmgMax) { dmgMax = false; }
        if (dmgUp) { dmgUp = false; }
    }

    public void DamageUp(){
		dmgUp = true;
	}

	public void DamageMax(){
		dmgMax = true;
	}

	public void HealDamage(){
		if (p1MechTeam) {
			myTurnCont.p1_health++;
		} else {
			myTurnCont.p2_health++;
		}
	}

	public void DodgeMove(){
		dodge = true;
	}

	public void Diagonal(){
		GameObject[] mechSpots = GameObject.FindGameObjectsWithTag ("Mech");
		for(int i = 0; i < 4; i++){
			if (mechSpots [i].GetComponent<PlayerMovement> ().posX == posX + 1 || mechSpots [i].GetComponent<PlayerMovement> ().posX == posX - 1) {
				if (mechSpots [i].GetComponent<PlayerMovement> ().posY == posY + 1 || mechSpots [i].GetComponent<PlayerMovement> ().posY == posY - 1) {
                    if (!mechSpots[i].GetComponent<PlayerMovement>().dodge)
                    {
                        if (!p1MechTeam)
                        {
                            myTurnCont.p1_health -= myDMG;
                        }
                        else {
                            myTurnCont.p2_health -= myDMG;
                        }
                    }

					Debug.Log ("Attack Hit! " + mechSpots [i].name + " was hit with 1 DMG!");
				}
			}
		}
        if (dmgMax) { dmgMax = false; }
        if (dmgUp) { dmgUp = false; }
    }

	public void Grenade(){
		GameObject[] mechSpots = GameObject.FindGameObjectsWithTag ("Mech");
		for(int i = 0; i < 4; i++){
            if (mechSpots[i].GetComponent<PlayerMovement>().posX <= posX + 1 && mechSpots[i].GetComponent<PlayerMovement>().posX >= posX - 1){
                if (mechSpots [i].GetComponent<PlayerMovement> ().posY <= posY + 1 && mechSpots [i].GetComponent<PlayerMovement> ().posY >= posY - 1) {
					if (!p1MechTeam) {
                        if (!mechSpots[i].GetComponent<PlayerMovement>().dodge)
                        {
                            myTurnCont.p1_health -= myDMG +1;
                        }
						myTurnCont.p2_health --;
					} else {
                        if (!mechSpots[i].GetComponent<PlayerMovement>().dodge)
                        {
                            myTurnCont.p2_health -= myDMG + 1;
                        }
						myTurnCont.p1_health--;
					}
				}
			}
		}
        if (dmgMax) { dmgMax = false; }
        if (dmgUp) { dmgUp = false; }
    }
}
