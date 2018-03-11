using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionManager : MonoBehaviour {

	Ball ball;
	List<Collision2D> collisions { get; set; }

	public Vector2 GetSumContactNormal() {
		Vector2 sumNormal = Vector2.zero;
		foreach (Collision2D collision in collisions)
			sumNormal += collision.contacts[0].normal;
		return sumNormal.normalized;
	}

	void Awake() {
		collisions = new List<Collision2D>();
		ball = GetComponent<Ball>();
	}

	void OnCollisionEnter2D(Collision2D collision2D) {
		collisions.Add(collision2D);
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
