using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public Dictionary<string, ItemSlot> slots = new Dictionary<string, ItemSlot>();
	public ItemSlot activeSlot;
	string _activeSlotName = "";
	public string activeSlotName {
		get {return _activeSlotName;}
		set {
			_activeSlotName = value;
			activeSlot = slots[value];
		}
	}

	public void init(string[] slotNames) {
		foreach (var slotName in slotNames) {
			slots.Add(slotName, new ItemSlot());
		}
	}
}
