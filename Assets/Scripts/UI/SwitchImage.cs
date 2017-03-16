using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchImage : MonoBehaviour {
	[SerializeField]
	Sprite image1;
	[SerializeField]
	Sprite image2;
	Image image;

	void Start() {
		image = gameObject.GetComponent<Image>();
	}

	public void switchImage() {
		if (image.sprite == image2) {
			image.sprite = image1;
		} else {
			image.sprite = image2;
		}
	}
}
