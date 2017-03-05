using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTile : Tile {
	public float oxygen = 0;
	public float carbonDioxide = 0;
	public float nitrogen = 0;
	public float nitrousOxide = 0;
	public float plasma = 0;

	protected override void Start() {
		gases["oxygen"] = oxygen;
		gases["carbonDioxide"] = carbonDioxide;
		gases["nitrogen"] = nitrogen;
		gases["nitrousOxide"] = nitrousOxide;
		gases["plasma"] = plasma;
		Tile.defaultTile = this;
	}

	protected override void FixedUpdate() {}
}
