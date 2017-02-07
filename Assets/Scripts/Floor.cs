using UnityEngine;
using System.Collections;

enum FloorLayer {
	LATTICE,
	CATWALK,
	PLATE,
	FLOOR,
}

public class Floor : Tile {
	public Sprite spriteLattice;
	public Sprite spriteCatwalk;
	public Sprite spritePlate;
	public Sprite spriteFloor;
	public Sprite spriteNitrousOxide;
	public Sprite spritePlasma;

	SpriteRenderer sr;
	FloorLayer _layer;
	FloorLayer layer {
		get {return _layer;}
		set {
			if (value == FloorLayer.FLOOR) {
				sr.sprite = spriteFloor;
			}
			else if (value == FloorLayer.PLATE) {
				sr.sprite = spritePlate;
			}
			_layer = value;
		}
	}

	// Use this for initialization
	new void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
		base.Start();
	}
	
	// Update is called once per frame
	void Update() {
		// Draw certain gases
		if (gases["nitrousOxide"] > 10) {
			sr.sprite = spriteNitrousOxide;
		} else
			layer = layer;

		if (gases["plasma"] > 10) {
			sr.sprite = spritePlasma;
		} else
			layer = layer;
	}
}
