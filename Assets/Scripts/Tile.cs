using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	// Physical properties
	public const float volume = 4; // m³
	public float temperature = 273; // Kelvin
	public float pressure = 0; // Pa
	public float totalMoles = 0;

	const float igc = 8.314f; // Ideal gas constant
	const float maxGasSpeed = 10; // Max gas speed in moles per FixedUpdate()

	// How much each gas this tile contains in moles.
	public Dictionary<string, float> gases = new Dictionary<string, float>();
	static string[] gasKeys = new string[] {
		"oxygen",
		"carbonDioxide",
		"nitrogen",
		"nitrousOxide",
		"plasma"
	};

	// Index 0 is right, values advance counter-clockwise
	Tile[] neighborTiles = new Tile[8];
	static Vector2[] neighborOffsets = new Vector2[] {
		new Vector2(1, 0),
		new Vector2(1, 1),
		new Vector2(0, 1),
		new Vector2(-1, 1),
		new Vector2(-1, 0),
		new Vector2(-1, -1),
		new Vector2(0, -1),
		new Vector2(1, -1),
	};

	protected void Start() {
		foreach (var key in gasKeys) {
			gases.Add(key, 0);
		}
	}

	void FixedUpdate() {
		// Apply gas physics
		totalMoles = 0;
		foreach (var entry in gases) {
			totalMoles += entry.Value;
		}
		pressure = calcPressure(totalMoles);
		checkNeighbors();
		foreach (var tile in neighborTiles) {
			if (tile != null) {
				// Move gases between tiles according to pressure
				if (tile.pressure < pressure) {
					float gasSpeed = (-(tile.totalMoles * tile.temperature - totalMoles * temperature)) / (tile.temperature + temperature);
					if (gasSpeed > maxGasSpeed) {
						gasSpeed = maxGasSpeed;
					}
					moveGases(gases, tile.gases, gasSpeed);
				}
			}
		}
	}

	float calcPressure(float moles) {
		return (moles * igc * temperature) / volume;
	}

	// Moves all gases from source to dest by maxMoveAmount
	// NOTE: Only pass non-negative moveAmounts.
	void moveGases(Dictionary<string, float> source, Dictionary<string, float> dest, float maxMoveAmount) {
		foreach (var key in gasKeys) {
			float moveAmount = maxMoveAmount;
			if (moveAmount > source[key]) {
				moveAmount = source[key];
			}
			source[key] -= moveAmount;
			dest[key] += moveAmount;
		}
	}

	// Check the existence of neighboring tiles.
	void checkNeighbors() {
		for (int i = 0; i < 8; ++i) {
			Collider2D result = Physics2D.OverlapCircle(
				                    new Vector2(transform.position.x + neighborOffsets[i].x,
				                                transform.position.y + neighborOffsets[i].y),
				                    0.0f);
			if (result == null) {
				neighborTiles[i] = null;
			} else {
				neighborTiles[i] = result.gameObject.GetComponent<Tile>();
			}
		}
	}
}
