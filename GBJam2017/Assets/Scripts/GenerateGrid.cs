using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {
	public int gridSize, numOfBuildings;
	public float cellWidth;
	public GameObject GridPiece,BuildingPiece,PlayerPiece;
	public GameObject[,] myMechGrid;
	public List<string> buildingAreas = new List<string>();

	// Use this for initialization
	void Start () {
		myMechGrid = new GameObject[gridSize, gridSize];

		StartGrid (); GenerateBuildings (); SpawnPlayers ();
		this.transform.position = Vector3.up * 24f;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void StartGrid(){
		for (int y = 0; y < gridSize; y++) {
			for (int x = 0; x < gridSize; x++) {
				//Color myCol = new Color (Random.value, Random.value, Random.value);
				Vector3 myPos = new Vector3 ((x - gridSize/2) * cellWidth + cellWidth/2, (y - gridSize/2) * cellWidth + cellWidth/2);
				GameObject currGridCell = GameObject.Instantiate (GridPiece, myPos, Quaternion.identity, this.transform.GetChild(0));
				currGridCell.name = "Cell (" + x + "," + y + ")";
				//currGridCell.GetComponent<SpriteRenderer> ().color = myCol;
				myMechGrid [x, y] = currGridCell;
			}
		}
	}

	void GenerateBuildings(){
		for (int i = 0; i < numOfBuildings; i++) {
			GameObject cellRef = myMechGrid [Random.Range (1, 5), Random.Range (1, 5)];
			GameObject newBuilding = GameObject.Instantiate (BuildingPiece, cellRef.transform.position, Quaternion.identity, this.transform.GetChild (1));
			newBuilding.name = "Building " + cellRef.name.Substring (cellRef.name.Length - 5);
			buildingAreas.Add(cellRef.name.Substring (cellRef.name.Length - 5));
			Destroy (cellRef);
		}
	}
	void SpawnPlayers(){
		GameObject pMechA1 = GameObject.Instantiate(PlayerPiece, myMechGrid[0,0].transform.position, Quaternion.identity, this.transform.GetChild(2));
		GameObject pMechA2 = GameObject.Instantiate(PlayerPiece, myMechGrid[0,5].transform.position, Quaternion.identity, this.transform.GetChild(2));
		GameObject pMechB1 = GameObject.Instantiate(PlayerPiece, myMechGrid[5,0].transform.position, Quaternion.identity, this.transform.GetChild(2));
		GameObject pMechB2 = GameObject.Instantiate(PlayerPiece, myMechGrid[5,5].transform.position, Quaternion.identity, this.transform.GetChild(2));

		pMechA1.GetComponent<PlayerMovement> ().TeamSet (true, 0, new Vector2(0,0));
		pMechA2.GetComponent<PlayerMovement> ().TeamSet (true, 1, new Vector2(0,5));
		pMechB1.GetComponent<PlayerMovement> ().TeamSet (false, 1, new Vector2(5,0));
		pMechB2.GetComponent<PlayerMovement> ().TeamSet (false, 0, new Vector2(5,5));

	}
}
