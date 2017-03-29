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

	public void Switch() {
		if (image.sprite == image2) {
			image.sprite = image1;
		} else {
			image.sprite = image2;
		}
	}
}
