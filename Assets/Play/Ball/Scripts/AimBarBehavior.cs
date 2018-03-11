using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBarBehavior : MonoBehaviour {

	[SerializeField] GameObject lineObject;

	void Start () {
		Hide ();
	}

	public void Show () {
		gameObject.SetActive (true);
	}

	public void Hide () {
		gameObject.SetActive (false);
	}

	public void UpdateAimBar (Vector2 compressionVector, Vector2 contactNormal) {
		float compressionRotation = Vector2.Angle (Vector2.right, compressionVector);
		if (Vector2.Dot (compressionVector, Vector2.up) < 0)
			compressionRotation *= -1;
		transform.rotation = Quaternion.Euler (0, 0, compressionRotation + 180);

		float scale = Mathf.Sqrt (Vector2.Dot (compressionVector, contactNormal) * -1);
		lineObject.transform.localScale = new Vector3 (20, scale * 4, 1);
		Color tempColor = lineObject.GetComponent<SpriteRenderer> ().color;
		tempColor.a = scale;
		lineObject.GetComponent<SpriteRenderer> ().color = tempColor;
	}
}
