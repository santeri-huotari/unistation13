using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : StationObject {
	public Item item;
	SpriteRenderer sr;

	void Start() {
		isPickable = true;
		sr = gameObject.GetComponent<SpriteRenderer>();
		sr.sprite = item.sprite;
	}
}
