using UnityEngine;

public class TestGasController : StationObject {
	[SerializeField]
	float plasmaEmitInterval = 0;
	[SerializeField]
	float noEmitInterval = 0;
	Floor tileBelow;

	void Start() {
		// Get a reference to the tile this gameobject is on top of.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.0f);
		foreach (var collider in colliders) {
			Floor tile = collider.gameObject.GetComponent<Floor>();
			if (tile != null) {
				tileBelow = tile;
				break;
			}
		}
	}

	void FixedUpdate() {
		// Emit gas
		tileBelow.Gases["plasma"] += plasmaEmitInterval;
		tileBelow.Gases["nitrousOxide"] += noEmitInterval;
	}
}
