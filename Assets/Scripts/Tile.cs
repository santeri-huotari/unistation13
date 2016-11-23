using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	// 'Physical' properties
	public int volume = 2500;
	public float temperature = 273; // Kelvin
	public int pressure = 0; // kPa

	// How much each gas this tile contains
	public int oxygen = 0;
	public int carbon_dioxide = 0;
}
