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

	SpriteRenderer sr;
	FloorLayer layer {
		get {return layer;}
		set {
			if (value == FloorLayer.FLOOR) {
				sr.sprite = spriteFloor;
			}
			else if (value == FloorLayer.PLATE) {
				sr.sprite = spritePlate;
			}
			layer = value;
		}
	}

	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
