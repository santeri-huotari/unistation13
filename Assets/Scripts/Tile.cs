using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	// Physical properties
	public const float volume = 2500;
	public float temperature = 273; // Kelvin
	public float pressure = 0; // Pa
	public float totalMoles = 0;
	public float startOxygen = 0;
	public float startNitrousOxide = 0;

	// How much each gas this tile contains in moles.
	public Dictionary<string, float> gases = new Dictionary<string, float>();

	const float igc = 8.314f; // Ideal gas constant
	BoxCollider2D collNeighbor;
	List<Tile> neighborTiles = new List<Tile>();

	protected void Start() {
		collNeighbor = gameObject.AddComponent<BoxCollider2D>();
		collNeighbor.size = new Vector2(1.9f, 1.9f);
		collNeighbor.isTrigger = true;

		/* PROBLEMATIC PART
		gases.Add("oxygen", 0.0f);
		gases.Add("carbonDioxide", 0.0f);
		gases.Add("nitrogen", 0.0f);
		gases.Add("nitrousOxide", 0.0f);
		gases.Add("plasma", 0.0f);
		 * PROBLEMATIC PART */

		gases["oxygen"] = startOxygen;
		gases["nitrousOxide"] = startNitrousOxide;
	}

	void FixedUpdate() {
		float gasSpeed = 10; // Max gas speed in moles per FixedUpdate()
		totalMoles = 0;
		foreach (var entry in gases) {
			totalMoles += entry.Value;
		}
		pressure = (totalMoles * igc * temperature) / volume;

		// Iterate all surrounding tiles and apply physics stuff
		foreach (var tile in neighborTiles) {
			// Move gases between tiles according to pressure
			if (tile.pressure > pressure) {
				// Determine gas multiplier
				float moveMultiplier = tile.pressure / pressure;
				if (moveMultiplier * totalMoles > gasSpeed) {
					moveMultiplier = gasSpeed / totalMoles;
				}
			}
		}
	}

	void moveGases(Dictionary<string, float> source, Dictionary<string, float> dest, float multiplier) {
		// Moves all gases from source to dest by multiplier
		float moveAmount;
		foreach (var entry in source) {
			moveAmount = multiplier * entry.Value;
			source[entry.Key] -= moveAmount;
			dest[entry.Key] += moveAmount;
		}
	}

	void OnTriggerEnter(Collider other) {
		Tile otherTile = other.gameObject.GetComponent<Tile>();
		if (neighborTiles.Contains(otherTile) && otherTile.gameObject.layer == 8) {
			neighborTiles.Add(otherTile);
		}
	}

	void OnTriggerExit(Collider other) {
		Tile otherTile = other.gameObject.GetComponent<Tile>();
		if (neighborTiles.Contains(otherTile)) {
			neighborTiles.Remove(otherTile);
		}
	}
}
