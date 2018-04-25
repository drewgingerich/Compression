using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHazard : MonoBehaviour {

	[SerializeField] GameObjectRuntimeSet ballGameObjects;

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject otherObject = collision.gameObject;
		if (ballGameObjects.items.Contains(otherObject))
			Destroy(otherObject);
	}
}
