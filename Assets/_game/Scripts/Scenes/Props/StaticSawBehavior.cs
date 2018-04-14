using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSawBehavior : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		Ball ball = other.GetComponent<Ball>();
		if (ball != null)
			ball.Die();
	}
}
