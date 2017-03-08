using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearablesToggle : MonoBehaviour {
	public GameObject wearableSlots;

	public void toggleEquipmentSlots() {
		wearableSlots.SetActive(!wearableSlots.activeSelf);
	}
}
