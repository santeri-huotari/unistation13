using UnityEngine;
using System.Collections;

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
	protected override void Start() {
		base.Start();
		sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update() {
		// Draw certain gases. Redraw layer sprite if there is not enough gas.
		if (gases["nitrousOxide"] > 10) {
			sr.sprite = spriteNitrousOxide;
		} else if (gases["plasma"] > 10) {
			sr.sprite = spritePlasma;
		} else {
			sr.sprite = spriteFloor;
		}
	}
}
