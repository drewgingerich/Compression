using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTrap : MonoBehaviour {

	[SerializeField] Rigidbody2D snapObject;
	[SerializeField] float snapTime;

	Vector3 originalPosition;
	bool armed = true;

	void Start() {
		originalPosition = snapObject.transform.position;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (armed) {
			armed = false;
			StartCoroutine(SnapObjectToPosition());
		}
	}

	IEnumerator SnapObjectToPosition() {
		yield return new WaitForFixedUpdate();
		float progress = 0f;
		while (true) {
			progress += Time.fixedDeltaTime / snapTime;
			Vector3 newPosition = Vector3.Lerp(originalPosition, transform.position, progress);
			snapObject.MovePosition(newPosition);
			if (progress >= 1)
				break;
			else
				yield return new WaitForFixedUpdate();
		}
	}
}
