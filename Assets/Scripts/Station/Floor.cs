using UnityEngine;

public class Floor : Tile {
	[SerializeField]
	Sprite spriteLattice;
	[SerializeField]
	Sprite spriteCatwalk;
	[SerializeField]
	Sprite spritePlate;
	[SerializeField]
	Sprite spriteFloor;
	[SerializeField]
	Sprite spriteNitrousOxide;
	[SerializeField]
	Sprite spritePlasma;

	SpriteRenderer spriteRenderer;
	FloorLayer layer;
	FloorLayer Layer {
		get {return layer;}
		set {
			if (value == FloorLayer.Floor) {
				spriteRenderer.sprite = spriteFloor;
			}
			else if (value == FloorLayer.Plate) {
				spriteRenderer.sprite = spritePlate;
			}
			layer = value;
		}
	}

	protected override void Start() {
		base.Start();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update() {
		// Draw certain gases. Redraw layer sprite if there is not enough gas.
		if (Gases["nitrousOxide"] > 10) {
			spriteRenderer.sprite = spriteNitrousOxide;
		} else if (Gases["plasma"] > 10) {
			spriteRenderer.sprite = spritePlasma;
		} else {
			spriteRenderer.sprite = spriteFloor;
		}
	}
}
