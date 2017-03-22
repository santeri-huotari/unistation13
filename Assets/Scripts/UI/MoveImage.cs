using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveImage : MonoBehaviour {
	[SerializeField]
	Transform firstTransform;
	[SerializeField]
	Transform secondTransform;
	[SerializeField]
	Transform movableTransform;

	public void moveImage() {
		if (movableTransform.position == firstTransform.position) {
			movableTransform.position = secondTransform.position;
		}
		else {
			movableTransform.position = firstTransform.position;
		}
	}
}
