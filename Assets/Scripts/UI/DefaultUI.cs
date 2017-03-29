using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour {
	[SerializeField]
	Player player;
	Dictionary<string, Image> uiSlotImages = new Dictionary<string, Image>();
	Color transparent = new Color(0, 0, 0, 0);

	void Awake() {
		// Map player inventory slots to respective UI elements.
		foreach (var slotName in Player.InventorySlotNames) {
			uiSlotImages.Add(slotName, GameObject.Find(slotName + "/Item").GetComponent<Image>());
		}
	}

	void Update() {
		// Update UI images
		foreach (var slotName in Player.InventorySlotNames) {
			Item item = player.Inventory.Slots[slotName].Item;
			if (item != null) {
				uiSlotImages[slotName].sprite = item.Sprite;
				uiSlotImages[slotName].color = Color.white;
			} else {
				uiSlotImages[slotName].color = transparent;
			}
		}
	}

	public void CallMethodFromPlayer(string methodName) {
		player.Invoke(methodName, 0.0f);
	}
}
