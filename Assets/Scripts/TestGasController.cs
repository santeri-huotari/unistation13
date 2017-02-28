using UnityEngine;
using System.Collections;

public class TestGasController : MonoBehaviour {
	public float plasmaEmitInterval = 0;
	public float noEmitInterval = 0;

	Floor tileBelow;

	void Start() {
		// Get a reference to the tile this gameobject is on top of.
		Collider2D[] colliderStack = Physics2D.OverlapCircleAll(transform.position, 0.0f);
		foreach (var collider in colliderStack) {
			Floor tile = collider.gameObject.GetComponent<Floor>();
			if (tile != null) {
				tileBelow = tile;
				break;
			}
		}
	}

	void FixedUpdate() {
		// Emit gas
		tileBelow.gases["plasma"] += plasmaEmitInterval;
		tileBelow.gases["nitrousOxide"] += noEmitInterval;
	}
}
