using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	public static ItemDatabase instance;
	public GameObject droppedItemPrefab;

	Dictionary<ItemID, Item> items = new Dictionary<ItemID, Item>();

	void Start() {
		if (instance == null) {
			instance = this;
		}
	}

	public static Item getItem(ItemID itemId) {
		return instance.items[itemId];
	}

	public GameObject getDroppedPrefab(Item item) {
		droppedItemPrefab.GetComponent<DroppedItem>().item = item;
		return droppedItemPrefab;
	}
}
