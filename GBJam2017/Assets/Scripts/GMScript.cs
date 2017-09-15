using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScript : MonoBehaviour {
	
	[System.Serializable]
	public class Mech {
		public string name;
		public int sprite_id;
		public int move_count;
		public string[] myCards = new string[6];

		public Mech(string n, int spr, int mov, string c1, string c2, string c3, string c4, string c5, string c6){
			name = n;
			sprite_id = spr;
			move_count = mov;
			myCards[0] = c1;
			myCards[1] = c2;
			myCards[2] = c3;
			myCards[3] = c4;
			myCards[4] = c5;
			myCards[5] = c6;
		}
	}

	[System.Serializable]
	public class Pilot {
		public string name;
		public int sprite_id;
		public string[] myCards = new string[6];

		public Pilot(string n, int spr, string c1, string c2, string c3, string c4, string c5, string c6){
			name = n;
			sprite_id = spr;
			myCards[0] = c1;
			myCards[1] = c2;
			myCards[2] = c3;
			myCards[3] = c4;
			myCards[4] = c5;
			myCards[5] = c6;
		}
	}

	public Mech[] gameMechs;
	public Pilot[] gamePilots;
	public Sprite[] pilotPortraits;

	[System.NonSerialized]
	public Mech p1_mech1, p1_mech2, p2_mech1, p2_mech2;
	[System.NonSerialized]
	public Pilot p1_pilot1, p1_pilot2, p2_pilot1, p2_pilot2;

	public List<string> p1CardDeck, p2CardDeck;

	// Use this for initialization
	void Start () {
		p1_mech1 = gameMechs [Random.Range (0, 3)];
		p1_mech2 = gameMechs [Random.Range (0, 3)];
		p2_mech1 = gameMechs [Random.Range (0, 3)];
		p2_mech2 = gameMechs [Random.Range (0, 3)];
		p1_pilot1 = gamePilots [Random.Range (0, 4)];
		p1_pilot2 = gamePilots [Random.Range (0, 4)];
		p2_pilot1 = gamePilots [Random.Range (0, 4)];
		p2_pilot2 = gamePilots [Random.Range (0, 4)];

		for (int i = 0; i < 6; i++) {
			p1CardDeck.Add (p1_mech1.myCards [i]);
			p1CardDeck.Add (p1_mech2.myCards [i]);
			p1CardDeck.Add (p1_pilot1.myCards [i]);
			p1CardDeck.Add (p1_pilot2.myCards [i]);

			p2CardDeck.Add (p2_mech1.myCards [i]);
			p2CardDeck.Add (p2_mech2.myCards [i]);
			p2CardDeck.Add (p2_pilot1.myCards [i]);
			p2CardDeck.Add (p2_pilot2.myCards [i]);
		}

		for (int i = 0; i < 2; i++){
			p1CardDeck.Add ("0_ATK SHRT");
			p1CardDeck.Add ("0_ATK SHRT");
			p1CardDeck.Add ("4_HEAL DMG");

			p2CardDeck.Add ("0_ATK SHRT");
			p2CardDeck.Add ("0_ATK SHRT");
			p2CardDeck.Add ("4_HEAL DMG");
		}

		GameObject.Find ("Pilot1A").transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = pilotPortraits[p1_pilot1.sprite_id];
		GameObject.Find ("Pilot1B").transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = pilotPortraits[p1_pilot2.sprite_id];
		GameObject.Find ("Pilot2A").transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = pilotPortraits[p2_pilot1.sprite_id];
		GameObject.Find ("Pilot2B").transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = pilotPortraits[p2_pilot2.sprite_id];
	}
	
	// Update is called once per frame
	void Update () {
	}
}