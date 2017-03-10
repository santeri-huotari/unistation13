using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour {
	public Player player;

	Dictionary<string, Image> uiSlotImages = new Dictionary<string, Image>();
	Color transparent = new Color(0, 0, 0, 0);

	void Awake() {
		// Map player inventory slots to respective UI elements.
		foreach (var slotName in Player.inventorySlotNames) {
			uiSlotImages.Add(slotName, GameObject.Find(slotName + "/Item").GetComponent<Image>());
		}
	}

	void Update() {
		// Update UI images
		foreach (var slotName in Player.inventorySlotNames) {
			Item item = player.inventory.slots[slotName].item;
			if (item != null) {
				uiSlotImages[slotName].sprite = item.sprite;
				uiSlotImages[slotName].color = Color.white;
			} else {
				uiSlotImages[slotName].color = transparent;
			}
		}
	}
}
