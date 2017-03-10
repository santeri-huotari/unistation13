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

	void Start() {
		slots.Add("HandLeft", new ItemSlot());
		slots.Add("HandRight", new ItemSlot());
		slots.Add("PocketLeft", new ItemSlot());
		slots.Add("PocketRight", new ItemSlot());
		slots.Add("Backpack", new ItemSlot());
		slots.Add("Belt", new ItemSlot());
		slots.Add("Id", new ItemSlot());
		slots.Add("SuitStorage", new ItemSlot());
		slots.Add("Shoes", new ItemSlot());
		slots.Add("InternalClothing", new ItemSlot());
		slots.Add("ExternalClothing", new ItemSlot());
		slots.Add("Gloves", new ItemSlot());
		slots.Add("Neck", new ItemSlot());
		slots.Add("Mask", new ItemSlot());
		slots.Add("Ears", new ItemSlot());
		slots.Add("Eyes", new ItemSlot());
		slots.Add("Head", new ItemSlot());
	}
}
