using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallCollisionManager : MonoBehaviour {

	Rigidbody2D rb2d;
	Ball ball;
	List<Collision2D> collisions { get; set; }
	Vector2 impactVector;

	void Awake() {
		collisions = new List<Collision2D>();
		ball = GetComponent<Ball>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		if (collisions.Count == 0)
			impactVector = rb2d.velocity;
		// Debug.Log(impactVector);
	}

	void OnCollisionEnter2D(Collision2D collision2D) {
		collisions.Add(collision2D);
		Vector2 contactNormal = GetContactNormal();
		ball.state.ContactNormal = contactNormal;
		if (collisions.Count == 1) {
			ball.state.ImpactMagnitude = impactVector.magnitude;
			ball.state.ReboundDirection = GetReboundDirection(impactVector, contactNormal);
			Debug.Log(ball.state.ReboundDirection);
			ball.state.Grounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision2D) {
		int i = 0;
		while (true) {
			if (collisions[i].collider == collision2D.collider)
				collisions.RemoveAt(i);
			else
				i++;
			if (i >= collisions.Count)
				break;
		}
		ball.state.ContactNormal = GetContactNormal();
		if (collisions.Count == 0) {
			ball.state.Grounded = false;
			impactVector = Vector2.zero;
		}
	}

	Vector2 GetContactNormal() {
		Vector2 sumNormal = Vector2.zero;
		foreach (Collision2D collision in collisions)
			sumNormal += collision.contacts[0].normal;
		return sumNormal.normalized;
	}

	Vector2 GetReboundDirection(Vector2 impactVector, Vector2 contactNormal) {
		Vector2 reboundVector = Vector2.Reflect(impactVector, contactNormal);
		return reboundVector.normalized;
	}
}
