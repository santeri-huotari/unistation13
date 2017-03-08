using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	// Surroundings
	public bool hasObstacle = false; // Wall, closed door etc
	List<StationObject> contents = new List<StationObject>(5);
	int maskDefault;
	int maskTile;

	// Physical properties
	public const float volume = 4; // m³
	public float temperature = 273; // Kelvin
	public float pressure = 0; // Pa
	public float totalMoles = 0;

	const float igc = 8.314f; // Ideal gas constant
	const float maxGasSpeed = 15; // Max gas speed in moles per FixedUpdate()

	// How much each gas this tile contains in moles.
	public Dictionary<string, float> gases = new Dictionary<string, float>();
	static string[] gasKeys = new string[] {
		"oxygen",
		"carbonDioxide",
		"nitrogen",
		"nitrousOxide",
		"plasma"
	};

	// Index 0 is right, values advance counter-clockwise.
	// Every null neighbor represents a default tile that
	// has an immutable gas composition eg. empty space.
	public static Tile defaultTile;
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

	protected virtual void Start() {
		maskDefault = LayerMask.GetMask("Default");
		maskTile = LayerMask.GetMask("Tile");
		foreach (var key in gasKeys) {
			gases.Add(key, 0);
		}
	}

	protected virtual void FixedUpdate() {
		checkSurroundings();
		checkNeighbors();

		// Apply gas physics
		totalMoles = 0;
		foreach (var entry in gases) {
			totalMoles += entry.Value;
		}
		pressure = (totalMoles * igc * temperature) / volume;
		foreach (var tile in neighborTiles) {
			if (tile != null) {
				if (!tile.hasObstacle) {
					moveGases(gases, tile.gases, calcMultiplier(tile));
				}
			} else {
				moveGases(gases, defaultTile.gases, calcMultiplier(defaultTile));
			}
		}
	}

	// Calculate gas movement multiplier
	float calcMultiplier(Tile otherTile) {
		if (otherTile.pressure < pressure) {
			float gasSpeed = (-(otherTile.totalMoles * otherTile.temperature - totalMoles * temperature)) / (otherTile.temperature + temperature);
			if (gasSpeed > maxGasSpeed) {
				gasSpeed = maxGasSpeed;
			}
			return gasSpeed / totalMoles;
		}
		return 0;
	}

	// Moves all gases from source to dest by multiplier
	// NOTE: Only pass multipliers in range [-1, 1]
	void moveGases(Dictionary<string, float> source, Dictionary<string, float> dest, float multiplier) {
		foreach (var key in gasKeys) {
			float moveAmount = multiplier * source[key];
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
				                    0.0f,
				                    maskTile);
			if (result == null) {
				neighborTiles[i] = null;
			} else {
				neighborTiles[i] = result.gameObject.GetComponent<Tile>();
			}
		}
	}

	// Get references to StationObjects on this tile.
	void checkSurroundings() {
		contents.Clear();
		hasObstacle = false;
		foreach (var collider in Physics2D.OverlapCircleAll((Vector2)transform.position, 0.0f, maskDefault)) {
			StationObject stationObject = collider.gameObject.GetComponent<StationObject>();
			if (stationObject.isObstacle) {
				hasObstacle = true;
			}
			contents.Add(stationObject);
		}
	}
}
