using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	public override BallController CheckTransitions(Ball ball) {
		if (ball.collisionManager.ballIsGrounded)
			return new GroundedController();
		return null;
	}

	public override void Enter(Ball ball) {
		if (ball.stickyManager.ballIsSticky)
			ball.stickyManager.DisableSticky(ball);
	}
	public override void Update(Ball ball) {
		// Vector2 inputDirection = ball.controlScheme.direction;
		// ball.gameObject.GetComponent<Rigidbody2D> ().AddForce (inputDirection * ball.speed);
	}
}
