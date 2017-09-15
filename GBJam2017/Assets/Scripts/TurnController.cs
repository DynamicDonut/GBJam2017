using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnController : MonoBehaviour {
	public int currTurn, currPhase, totalTurns, p1_health, p2_health, tHealth;
	public string currTurnPhase;
	public TextMeshProUGUI UIDialogueText;
	public TextMeshPro TurnCardText;
	string[] turnPhases = new string[7]{"PickCards1", "MoveMechsA1", "MoveMechsA2", "PickCards2", "MoveMechsB1", "MoveMechsB2", "ExecuteActions"};
	public GameObject[] listOfMechs;

	public Transform myGrid, myCards, myUI;

	// Use this for initialization
	void Start () {
		currTurn = 1;
		currPhase = 0;
		currTurnPhase = turnPhases [currPhase];

		myGrid = GameObject.Find ("GridGen").transform;
		myCards = GameObject.Find ("CardBG").transform;

		p1_health = p2_health = tHealth;
		myUI.Find ("P1 Info").GetChild (2).GetComponent<TextMeshPro> ().text = p1_health + " / " + tHealth;
		myUI.Find ("P2 Info").GetChild (2).GetComponent<TextMeshPro> ().text = p2_health + " / " + tHealth;
	}
	
	// Update is called once per frame
	void Update () {
		listOfMechs = GameObject.FindGameObjectsWithTag ("Mech");
		if (currPhase == 6){
			EndTurn ();
		}
		if (p1_health < 0) {
			p1_health = 0;
		}

		if (p2_health < 0) {
			p2_health = 0;
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			currPhase++;
		}

		if (currPhase >= turnPhases.Length) {
			currTurn++;
			currPhase = 0;
		}

		currTurnPhase = turnPhases [currPhase];
		UIDialogueText.text = currTurnPhase + " of Turn " + currTurn;
		Debug.Log (currTurnPhase + " of Turn " + currTurn);
	}

	public void EndTurn(){
		for (int i = 0; i < listOfMechs.Length; i++) {
			listOfMechs [i].GetComponent<PlayerMovement> ().AttackShortRange (listOfMechs [i].GetComponent<PlayerMovement> ().posX, listOfMechs [i].GetComponent<PlayerMovement> ().posY);
		}

		myUI.Find ("P1 Info").GetChild (2).GetComponent<TextMeshPro> ().text = p1_health + " / " + tHealth;
		myUI.Find ("P2 Info").GetChild (2).GetComponent<TextMeshPro> ().text = p2_health + " / " + tHealth;
		currTurn++;
		currPhase = 0;
	}
}
