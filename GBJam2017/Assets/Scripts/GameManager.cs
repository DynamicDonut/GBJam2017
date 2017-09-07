using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject cardFolder;
	public float spd;
	Vector3 hiddenCardsPos;
	bool amHidden;
	// Use this for initialization
	void Start () {
		hiddenCardsPos = cardFolder.transform.position;
		amHidden = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			amHidden = false;
		}

		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			amHidden = true;
		}

		if (!amHidden) {
			if (cardFolder.transform.position != Vector3.zero) {
				cardFolder.transform.position = Vector3.MoveTowards (cardFolder.transform.position, Vector3.zero, spd * Time.deltaTime);
			}
		} else {
			if (cardFolder.transform.position != hiddenCardsPos) {
				cardFolder.transform.position = Vector3.MoveTowards (cardFolder.transform.position, hiddenCardsPos, spd * Time.deltaTime);
			}
		}
	}
}
