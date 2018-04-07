using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallCollisionManager : MonoBehaviour {

	Rigidbody2D rb2d;
	Ball ball;
	int maxContactPoints = 10;
	Vector2 impactVector;

	void Awake() {
		ball = GetComponent<Ball>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		int numberOfContacts = rb2d.GetContacts(new ContactPoint2D[0]);
		if (numberOfContacts == 0)
			impactVector = rb2d.velocity;
		if (numberOfContacts > 0)
			ball.state.ContactNormal = GetContactNormal();
		Debug.Log(ball.state.ContactNormal);
	}

	void OnCollisionEnter2D(Collision2D collision2D) {
		int numberOfContacts = rb2d.GetContacts(new ContactPoint2D[0]);
		Vector2 contactNormal = GetContactNormal();
		ball.state.ContactNormal = contactNormal;
		if (!ball.state.Grounded) {
			ball.state.ImpactMagnitude = impactVector.magnitude;
			ball.state.ReboundDirection = GetReboundDirection(impactVector, contactNormal);
			ball.state.Grounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision2D) {
		int numberOfContacts = rb2d.GetContacts(new ContactPoint2D[0]);
		if (numberOfContacts == 0) {
			ball.state.Grounded = false;
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
