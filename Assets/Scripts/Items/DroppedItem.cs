using UnityEngine;

public class DroppedItem : StationObject {
	public Item Item;
	SpriteRenderer spriteRenderer;

	void Start() {
		IsPickable = true;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = Item.Sprite;
	}
}
