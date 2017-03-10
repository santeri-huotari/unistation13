using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot {
	Item _item;
	public Item item {
		get {return _item;}
		set {
			if (value.size <= this.size) {
				_item = value;
			}
		}
	}

	int size = 1;

	public bool isEmpty() {
		return item == null;
	}
}
