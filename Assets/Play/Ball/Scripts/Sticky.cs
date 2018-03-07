using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour {

	Ball ball;
	new Rigidbody2D rigidbody;

	public float gravity = 0.3f;
	public float stickyTime = 0f;

	void Awake() {
		ball = GetComponent<Ball>();
		rigidbody = GetComponent<Rigidbody2D>();
	}

	public void EnableSticky() {
		ball.state.sticky = true;
		rigidbody.gravityScale = 0;
	}

	public void DisableSticky() {
		ball.state.sticky = false;
		rigidbody.gravityScale = gravity;
		stickyTime = 0f;
	}
}
