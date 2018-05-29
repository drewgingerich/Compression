using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundController : CompressedController {

	float maxTimeCompressed = 1f;

	public override void Update(BallState state, Rigidbody2D rb2d) {
		BallActions.ApplyFriction(state, rb2d);
		BallActions.ApplyStickiness(state, rb2d);
		BallActions.Aim(state, rb2d);
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Rebound;
		state.frictionMagnitude.Value = 10f;
		BallActions.SetAim(state, rb2d);
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Rebound;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckLaunchTransition(state)) {
			BallActions.LaunchBall(state, rb2d);
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
