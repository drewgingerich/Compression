using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : BallController {

	Ball ball;
	// float maxStickyTime = 0.3f;

	public GroundedController (Ball ball) {
		this.ball = ball;
	}

	public override BallController CheckTransitions() {
		if (!ball.state.grounded) {
			return new AirbornController (ball);
		}
		Vector2 inputDirection = ball.inputScheme.GetInputDirection();
		Vector2 sumNormal = ball.collisionManager.GetSumContactNormal ();
		if (Vector2.Dot (inputDirection, sumNormal) < 0) {
			return new CompressedController (ball);
		}
		return null;
	}

	public override void Enter() {
		ball.state.timeGrounded = 0f;
	}

	public override void Update () {
		ball.state.timeGrounded += Time.deltaTime;
		// Vector2 inputDirection = ball.controlScheme.direction;
		// ball.gameObject.GetComponent<Rigidbody2D> ().AddForce (inputDirection);
		// if (ball.sticky) {
		// 	ball.stickyTime += Time.deltaTime;
		// 	if (ball.stickyTime >= maxStickyTime)
		// 		ball.DisableSticky ();
		// }
	}
}
