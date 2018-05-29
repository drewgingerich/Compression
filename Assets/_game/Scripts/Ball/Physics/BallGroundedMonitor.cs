using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGroundedMonitor : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] Rigidbody2D rb2d;

	int numContacts = 0;

	void OnTriggerEnter2D(Collider2D other) {
		numContacts++;
		ball.state.grounded.Value = true;
		ball.state.timeGrounded.Value = 0f;
	}

	void OnTriggerExit2D(Collider2D other) {
		numContacts--;
		if (numContacts == 0)
			ball.state.grounded.Value = false;
	}
}
