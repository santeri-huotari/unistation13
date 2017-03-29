using UnityEngine;

public class DefaultTile : Tile {
	[SerializeField]
	float oxygen = 0;
	[SerializeField]
	float carbonDioxide = 0;
	[SerializeField]
	float nitrogen = 0;
	[SerializeField]
	float nitrousOxide = 0;
	[SerializeField]
	float plasma = 0;

	protected override void Start() {
		Gases["oxygen"] = oxygen;
		Gases["carbonDioxide"] = carbonDioxide;
		Gases["nitrogen"] = nitrogen;
		Gases["nitrousOxide"] = nitrousOxide;
		Gases["plasma"] = plasma;
		Tile.DefaultTile = this;
	}

	protected override void FixedUpdate() {}
}
