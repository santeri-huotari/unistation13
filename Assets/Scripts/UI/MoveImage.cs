using UnityEngine;

public class MoveImage : MonoBehaviour {
	[SerializeField]
	Transform firstTransform;
	[SerializeField]
	Transform secondTransform;
	[SerializeField]
	Transform movableTransform;

	public void Move() {
		if (movableTransform.position == firstTransform.position) {
			movableTransform.position = secondTransform.position;
		}
		else {
			movableTransform.position = firstTransform.position;
		}
	}
}
