using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public Dictionary<string, ItemSlot> Slots = new Dictionary<string, ItemSlot>();
	public ItemSlot ActiveSlot;
	string activeSlotName = "";
	public string ActiveSlotName {
		get {return activeSlotName;}
		set {
			activeSlotName = value;
			ActiveSlot = Slots[value];
		}
	}

	public void Init(string[] slotNames) {
		foreach (var slotName in slotNames) {
			Slots.Add(slotName, new ItemSlot());
		}
	}
}
