using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	// Surroundings
	public bool HasObstacle = false; // Wall, closed door etc
	public List<StationObject> Contents = new List<StationObject>(5);
	int defaultMask;
	int tileMask;

	// Physical properties
	public const float Volume = 4; // m³
	public float Temperature = 273; // Kelvin
	public float Pressure = 0; // Pa
	public float TotalMoles = 0;

	const float igc = 8.314f; // Ideal gas constant
	const float maxGasSpeed = 15; // Max gas speed in moles per FixedUpdate()

	// How much each gas this tile contains in moles.
	public Dictionary<string, float> Gases = new Dictionary<string, float>();
	static readonly string[] gasKeys = {
		"oxygen",
		"carbonDioxide",
		"nitrogen",
		"nitrousOxide",
		"plasma"
	};

	// Index 0 is right, values advance counter-clockwise.
	// Every null neighbor represents a default tile that
	// has an immutable gas composition eg. empty space.
	public static Tile DefaultTile;
	public Tile[] NeighborTiles = new Tile[8];
	static readonly Vector2[] neighborOffsets = {
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
		defaultMask = LayerMask.GetMask("Default");
		tileMask = LayerMask.GetMask("Tile");
		foreach (var key in gasKeys) {
			Gases.Add(key, 0);
		}
	}

	protected virtual void FixedUpdate() {
		CheckSurroundings();
		CheckNeighbors();
		ApplyGasPhysics();
	}

	void ApplyGasPhysics() {
		TotalMoles = 0;
		foreach (var entry in Gases) {
			TotalMoles += entry.Value;
		}
		Pressure = (TotalMoles * igc * Temperature) / Volume;
		foreach (var tile in NeighborTiles) {
			if (tile != null) {
				if (!tile.HasObstacle) {
					MoveGases(Gases, tile.Gases, CalcMultiplier(tile));
				}
			} else {
				MoveGases(Gases, DefaultTile.Gases, CalcMultiplier(DefaultTile));
			}
		}
	}

	/// <summary>
	/// Calculate gas movement multiplier.
	/// </summary>
	/// <returns>The multiplier.</returns>
	/// <param name="otherTile">Other tile.</param>
	float CalcMultiplier(Tile otherTile) {
		if (otherTile.Pressure < Pressure) {
			float gasSpeed = (-(otherTile.TotalMoles * otherTile.Temperature - TotalMoles * Temperature)) / (otherTile.Temperature + Temperature);
			if (gasSpeed > maxGasSpeed) {
				gasSpeed = maxGasSpeed;
			}
			return gasSpeed / TotalMoles;
		}
		return 0;
	}

	/// <summary>
	/// Moves all gases from source to dest by multiplier.
	/// NOTE: Only pass multipliers in range [-1, 1].
	/// </summary>
	/// <param name="source">Source.</param>
	/// <param name="dest">Destination.</param>
	/// <param name="multiplier">Multiplier.</param>
	void MoveGases(Dictionary<string, float> source, Dictionary<string, float> dest, float multiplier) {
		foreach (var key in gasKeys) {
			float moveAmount = multiplier * source[key];
			source[key] -= moveAmount;
			dest[key] += moveAmount;
		}
	}

	/// <summary>
	/// Check the existence of neighboring tiles.
	/// </summary>
	void CheckNeighbors() {
		for (int i = 0; i < 8; ++i) {
			Collider2D result = Physics2D.OverlapCircle(
				                    new Vector2(transform.position.x + neighborOffsets[i].x,
				                                transform.position.y + neighborOffsets[i].y),
				                    0.0f,
				                    tileMask);
			if (result == null) {
				NeighborTiles[i] = null;
			} else {
				NeighborTiles[i] = result.gameObject.GetComponent<Tile>();
			}
		}
	}

	/// <summary>
	/// Get references to StationObjects on this tile.
	/// </summary>
	void CheckSurroundings() {
		Contents.Clear();
		HasObstacle = false;
		foreach (var collider in Physics2D.OverlapCircleAll((Vector2)transform.position, 0.0f, defaultMask)) {
			StationObject stationObject = collider.gameObject.GetComponent<StationObject>();
			if (stationObject.IsObstacle) {
				HasObstacle = true;
			}
			Contents.Add(stationObject);
		}
	}
}
