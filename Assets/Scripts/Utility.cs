using UnityEngine;

public static class Utility {

	public static Vector3 RoundVector(Vector3 vector) {
		return new Vector3(Mathf.Round(vector.x),
		                   Mathf.Round(vector.y),
		                   Mathf.Round(vector.z));
	}

	public static Vector2 RoundVector(Vector2 vector) {
		return new Vector2(Mathf.Round(vector.x),
		                   Mathf.Round(vector.y));
	}
}
