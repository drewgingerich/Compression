using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundController : CompressedController {

	float maxTimeCompressed;

	public ReboundController() : base() {
		maxTimeCompressed = 0.5f;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Rebound;
		state.compressionDirection.Value = state.inputDirection.Value.Clamp(-state.contactNormal.Value, maxLaunchAngle);
		state.gravityRatio.Value = 0f;
		rb2d.velocity = Vector2.zero;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Rebound;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckLaunchTransition(state) || CheckTimeoutTransition(state)) {
			LaunchBall(state, rb2d, -state.compressionDirection.Value);
			return new AirbornController();
		}
		return null;
	}

	bool CheckTimeoutTransition(BallState state) {
		return state.timeInState.Value >= maxTimeCompressed ? true : false;
	}
}
