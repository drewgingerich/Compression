using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionDetector : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] Rigidbody2D rb2d;

	int maxContactPoints = 10;
	Vector2 impactVector;
	Vector2 lastContactNormal;

	void FixedUpdate () {
		int numberOfContacts = rb2d.GetContacts(new ContactPoint2D[1]);
		if (numberOfContacts == 0)
			impactVector = rb2d.velocity;
		if (numberOfContacts > 0) {
			Vector2 normal = GetContactNormal();
			if (normal == lastContactNormal)
				return;
			lastContactNormal = normal;
			ball.state.contactNormal.Value = normal;
		}
	}

	void OnCollisionEnter2D(Collision2D collision2D) {
		Vector2 normal = GetContactNormal();
		lastContactNormal = normal;
		ball.state.contactNormal.Value = normal;
		int numberOfContacts = rb2d.GetContacts(new ContactPoint2D[1]);
		if (numberOfContacts == 1) {
			ball.state.grounded.Value = true;
			ball.state.timeGrounded.Value = 0f;
			ball.state.impactMagnitude.Value = impactVector.magnitude;
			ball.state.airjumpAvailable.Value = true;
			ball.state.reboundDirection.Value = GetReboundDirection(impactVector, normal);
		}
	}

	void OnCollisionExit2D(Collision2D collision2D) {
		int numberOfContacts = rb2d.GetContacts(new ContactPoint2D[1]);
		if (numberOfContacts == 0) {
			ball.state.grounded.Value = false;
			ball.state.timeAirborn.Value = 0f;
			impactVector = Vector2.zero;
		}
	}

	Vector2 GetContactNormal() {
		ContactPoint2D[] contactPoints = new ContactPoint2D[maxContactPoints];
		rb2d.GetContacts(contactPoints);
		Vector2 sumNormal = Vector2.zero;
		foreach (ContactPoint2D contact in contactPoints)
			sumNormal += contact.normal;
		return sumNormal.normalized;
	}

	Vector2 GetReboundDirection(Vector2 impactVector, Vector2 contactNormal) {
		Vector2 reboundVector = Vector2.Reflect(impactVector, contactNormal);
		return reboundVector.normalized;
	}
}
