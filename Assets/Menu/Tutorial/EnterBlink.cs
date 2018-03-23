using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterBlink : MonoBehaviour {

	Image image;
	float blinkTimer = 0;
	float onInterval = 0.5f;
	float offInterval = 0.45f;

	void Start () {
		image = gameObject.GetComponent<Image> ();
	}

	void Update () {
		blinkTimer += Time.deltaTime;
		if (image.enabled && blinkTimer >= onInterval) {
			blinkTimer = 0;
			image.enabled = false;
		} else if (!image.enabled && blinkTimer >= offInterval) {
			blinkTimer = 0;
			image.enabled = true;
		}
	}
}
