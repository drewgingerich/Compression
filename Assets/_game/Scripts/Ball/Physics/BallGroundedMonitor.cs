using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGroundedMonitor : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] Rigidbody2D rb2d;
	[SerializeField] Collider2D triggerCollider;

	int numContacts = 0;
	List<Collider2D> contacts = new List<Collider2D>();

	void FixedUpdate() {
		Vector2 contactDirection = Vector2.zero;
		foreach (Collider2D contact in contacts) {
			contactDirection += triggerCollider.Distance(contact).normal;
		}
		ball.state.contactNormal.Value = -contactDirection.normalized;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!contacts.Contains(other)) {
			contacts.Add(other);
			numContacts++;
		}
		if (contacts.Count == 1) {
			ball.state.grounded.Value = true;
			ball.state.timeGrounded.Value = 0f;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (contacts.Contains(other)) {
			contacts.Remove(other);
			numContacts--;
		}
		if (numContacts == 0)
			ball.state.grounded.Value = false;
	}
}
