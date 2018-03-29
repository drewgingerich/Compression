using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	float timeInState;
	float lag = 0.1f;

	public override BallController CheckTransitions(Ball ball) {
		if (CheckStickyGroundedTransition(ball))
			return new StickyGroundedController();
		return null;
	}

	public override void Enter(Ball ball) {
		Rigidbody2D rb2d = ball.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 1f;
		timeInState = 0f;
	}

	// public override void Update(Ball ball) {
		// Vector2 inputDirection = ball.controlScheme.direction;
		// ball.gameObject.GetComponent<Rigidbody2D> ().AddForce (inputDirection * ball.speed);
	// }

	bool CheckStickyGroundedTransition(Ball ball) {
		timeInState += Time.deltaTime;
		return ball.collisionManager.ballIsGrounded && timeInState >= lag;
	}
}
