using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	public static ItemDatabase Instance;
	[SerializeField]
	GameObject droppedItemPrefab;

	Dictionary<ItemID, Item> items = new Dictionary<ItemID, Item>();

	void Start() {
		if (Instance == null) {
			Instance = this;
		}
	}

	public static Item GetItem(ItemID itemId) {
		return Instance.items[itemId];
	}

	public GameObject GetDroppedPrefab(Item item) {
		droppedItemPrefab.GetComponent<DroppedItem>().Item = item;
		return droppedItemPrefab;
	}
}
