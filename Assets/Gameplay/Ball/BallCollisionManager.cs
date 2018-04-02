using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallCollisionManager : MonoBehaviour {

	Ball ball;
	List<Collision2D> collisions { get; set; }



	void Awake() {
		collisions = new List<Collision2D>();
		ball = GetComponent<Ball>();
	}

	void OnCollisionEnter2D(Collision2D collision2D) {
		collisions.Add(collision2D);
		if (collisions.Count == 1) {
			ball.state.ImpactMagnitude = collision2D.relativeVelocity.magnitude;
			ball.state.ReboundDirection = GetReboundDirection(collision2D.relativeVelocity);
			ball.state.Grounded = true;
		}
		ball.state.ContactNormal = GetContactNormal();
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
		}
	}

	Vector2 GetContactNormal() {
		Vector2 sumNormal = Vector2.zero;
		foreach (Collision2D collision in collisions)
			sumNormal += collision.contacts[0].normal;
		return sumNormal.normalized;
	}

	Vector2 GetReboundDirection(Vector2 impactVector) {
		return Vector2.Reflect(impactVector, GetContactNormal()).normalized * -1;
	}
}
