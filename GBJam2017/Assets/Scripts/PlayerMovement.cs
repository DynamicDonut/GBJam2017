using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public int posX, posY, moveCounter;
	GenerateGrid myGridScript;
	bool ignoreMove;

	// Use this for initialization
	void Start () {
		posX = posY = 0;
		moveCounter = Random.Range (3, 6);
		ignoreMove = false;
		myGridScript = GameObject.Find ("GridGen").GetComponent<GenerateGrid> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (moveCounter != 0) {
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				if (posX < myGridScript.gridSize) {
					posX++;
				}
				MovementCheck ("right");
			}
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				if (posX > 0) {
					posX--;
				}
				MovementCheck ("left");
			}
			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				if (posY < myGridScript.gridSize) {
					posY++;
				}
				MovementCheck ("up");
			}
			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				if (posY > 0) {
					posY--;
				}
				MovementCheck ("down");
			}
		}

		if (Input.GetKeyUp (KeyCode.Z)) {
			AttackCloseRange (posX, posY);
		}
	}

	void MovementCheck(string dir){
		string areaTest = "(" + posX + "," + posY + ")";
		foreach (string cell in myGridScript.buildingAreas) {
			if (areaTest == cell) {
				ignoreMove = true;
			}
		}

		if (!ignoreMove) {
			Vector3 newPos = GameObject.Find ("Cell (" + posX + "," + posY + ")").transform.position;
			moveCounter--;
			transform.position = newPos;
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

	void AttackCloseRange(int myX, int myY){
		GameObject.Find ("Cell (" + (myX - 1) + "," + myY + ")").GetComponent<SpriteRenderer> ().color = Color.black;
		GameObject.Find ("Cell (" + (myX + 1) + "," + myY + ")").GetComponent<SpriteRenderer> ().color = Color.black;
		GameObject.Find ("Cell (" + myX + "," + (myY - 1) + ")").GetComponent<SpriteRenderer> ().color = Color.black;
		GameObject.Find ("Cell (" + myX + "," + (myY + 1) + ")").GetComponent<SpriteRenderer> ().color = Color.black;
	}
}
