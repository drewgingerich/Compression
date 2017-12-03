using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBarBehavior : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		gameObject.SetActive (false);
	}

	public void ShowBar () {
		gameObject.SetActive (true);
	}

	public void HideBar () {
		gameObject.SetActive (false);
	}

	public void UpdateAimBar (Vector2 compressionVector, Vector2 contactNormal) {
		float compressionRotation = Vector2.Angle (Vector2.right, compressionVector);
		if (Vector2.Dot (compressionVector, Vector2.up) < 0)
			compressionRotation *= -1;
		transform.rotation = Quaternion.Euler (0, 0, compressionRotation + 180);
	}
}
