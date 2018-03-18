using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionManager : MonoBehaviour {

	[SerializeField] Ball ball;
	List<Collision2D> collisions { get; set; }
	Vector2 impactVector;

	public Vector2 GetReflectionVector() {
		return Vector2.Reflect(-impactVector.normalized, GetSumContactNormal());
	}

	public Vector2 GetSumContactNormal() {
		Vector2 sumNormal = Vector2.zero;
		foreach (Collision2D collision in collisions)
			sumNormal += collision.contacts[0].normal;
		return sumNormal.normalized;
	}

	void Awake() {
		collisions = new List<Collision2D>();
	}

	void OnCollisionEnter2D(Collision2D collision2D) {
		collisions.Add(collision2D);
		if (!ball.state.grounded)
			impactVector = collision2D.relativeVelocity;
		ball.state.grounded = true;
	}

	void OnCollisionExit2D(Collision2D collision2D) {
		int i = 0;
		while (true) {
			if (collisions[i].collider == collision2D.collider)
				collisions.RemoveAt(i);
			else
				i++;
			if (i < collisions.Count)
				continue;
			else
				break;
		}
		if (collisions.Count == 0) {
			// DisableSticky();
			ball.state.grounded = false;
		}
	}
}
