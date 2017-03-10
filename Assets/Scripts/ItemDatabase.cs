using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	public static ItemDatabase instance;

	Dictionary<ItemID, Item> items = new Dictionary<ItemID, Item>();

	void Start() {
		if (instance == null) {
			instance = this;
		}
	}

	public static Item getItem(ItemID itemId) {
		return instance.items[itemId];
	}
}
