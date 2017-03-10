using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearablesToggle : MonoBehaviour {
	public GameObject wearableSlots;

	void Start() {
		wearableSlots.SetActive(false);
	}

	public void toggleEquipmentSlots() {
		wearableSlots.SetActive(!wearableSlots.activeSelf);
	}
}
