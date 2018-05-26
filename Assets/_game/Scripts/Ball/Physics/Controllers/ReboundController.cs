using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundController : CompressedController {

	float maxTimeCompressed;

	public ReboundController() : base() {
		maxTimeCompressed = 1f;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Rebound;
		state.compressionDirection.Value = state.inputDirection.Value.ClampRotation(-state.contactNormal.Value, maxLaunchAngle);
		state.gravityRatio.Value = 0f;
		// rb2d.velocity = Vector2.zero;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Rebound;
	}

	// public override void Update(BallState state, Rigidbody2D rb2d) {
	// 	base.Update(state, rb2d);
	// 	float alignmentWithGravity = Vector2.Dot(rb2d.velocity.normalized, Physics2D.gravity.normalized);
	// 	Debug.Log(alignmentWithGravity);
	// 	rb2d.AddForce(rb2d.velocity * -state.friction.Value * 4 * (alignmentWithGravity * -0.5f + 1));
	// }

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckLaunchTransition(state)) {
			BallActions.LaunchBall(state, rb2d, -state.compressionDirection.Value);
			state.freshInput.Value = false;
			return new AirbornController();
		}
		if (CheckTimeoutTransition(state)) {
			return new CompressedController();
		}
		return null;
	}

	bool CheckTimeoutTransition(BallState state) {
		return state.timeInState.Value >= maxTimeCompressed ? true : false;
	}
}
