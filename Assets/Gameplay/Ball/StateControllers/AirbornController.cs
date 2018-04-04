using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	float timeInState;
	float lag = 0.2f;

	public override BallController CheckTransitions(Ball ball) {
		if (CheckStickyGroundedTransition(ball))
			return new StickyGroundedController();
		return null;
	}

	public override void Enter(Ball ball) {
		ball.state.CurrentGravity = ball.state.BaseGravity;
		timeInState = 0f;
	}

	// public override void Update(Ball ball) {
	// 	if (Vector2.Dot(ball.rb2d.velocity, Vector2.up) > 0)
	// 		ball.state.CurrentGravity = 1f;
	// 	else
	// 		ball.state.CurrentGravity = 1f;
		// Vector2 inputDirection = ball.controlScheme.direction;
		// ball.gameObject.GetCompoent<Rigidbody2D> ().AddForce (inputDirection * ball.speed);
	// }

	bool CheckStickyGroundedTransition(Ball ball) {
		timeInState += Time.deltaTime;
		return ball.state.Grounded && timeInState >= lag;
	}
}
