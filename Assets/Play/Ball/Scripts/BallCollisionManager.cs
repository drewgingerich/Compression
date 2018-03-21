using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionManager : MonoBehaviour {

	public bool ballIsGrounded;
	public float timeGrounded;
	public Vector2 impactVector { get; private set; }

	List<Collision2D> collisions { get; set; }

	public Vector2 GetReboundVector() {
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
		if (!ballIsGrounded)
			impactVector = collision2D.relativeVelocity;
		ballIsGrounded = true;
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
		if (collisions.Count == 0) {
			ballIsGrounded = false;
		}
	}
}
