using UnityEngine;

public class WearablesToggle : MonoBehaviour {
	[SerializeField]
	GameObject wearableSlots;

	void Start() {
		wearableSlots.SetActive(false);
	}

	public void ToggleEquipmentSlots() {
		wearableSlots.SetActive(!wearableSlots.activeSelf);
	}
}
