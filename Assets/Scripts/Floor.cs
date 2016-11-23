using UnityEngine;
using System.Collections;

enum FloorType {
	FLOOR,
	PANEL,
	GRID
}

public class Floor : MonoBehaviour {
	// How much each gas this tile contains
	public float oxygen = 0;
	public float carbon_dioxide = 0;

	FloorType layer = FloorType.FLOOR;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
