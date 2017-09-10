using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnController : MonoBehaviour {

	public int currTurn, currPhase, totalTurns;
	public string currTurnPhase;
	public TextMeshProUGUI UIDialogueText;
	public TextMeshPro TurnCardText;
	string[] turnPhases = new string[7]{"PickCards1", "MoveMechsA1", "MoveMechsA2", "PickCards2", "MoveMechsB1", "MoveMechsB2", "ExecuteActions"};

	// Use this for initialization
	void Start () {
		currTurn = 1;
		currPhase = 0;
		currTurnPhase = turnPhases [currPhase];
	}
	
	// Update is called once per frame
	void Update () {
		currTurnPhase = turnPhases [currPhase];
		UIDialogueText.text = currTurnPhase + " of Turn " + currTurn;
		Debug.Log (currTurnPhase + " of Turn " + currTurn);

		if (Input.GetKeyUp (KeyCode.Space)) {
			currPhase++;
			if (currPhase >= turnPhases.Length) {
				currTurn++;
				currPhase = 0;
			}
		}
	}
}
