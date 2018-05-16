using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirjumpController : CompressedController {

	float maxTimeCompressed;

	public AirjumpController() : base() {
		maxTimeCompressed = 0.05f;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Airjump;
		state.reboundDirection.Value = rb2d.velocity.normalized;
		state.compressionDirection.Value = state.inputDirection.Value;
		rb2d.velocity = Vector2.zero;
		state.airjumpAvailable.Value = false;
		state.gravityRatio.Value = 0;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.impactMagnitude.Value = 0f;
		state.previousState.Value = StateName.Airjump;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckImpactTransition(state))
			return new ImpactController();
		if (CheckLaunchTransition(state) || CheckTimeoutTransition(state)) {
			LaunchBall(state, rb2d, -state.compressionDirection.Value);
			state.freshInput.Value = false;
			return new LaunchedController();
		}
		return null;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		state.compressionDirection.Value = state.inputDirection.Value;
	}

	bool CheckImpactTransition(BallState state) {
		return state.grounded.Value;
	}

	bool CheckTimeoutTransition(BallState state) {
		return state.timeInState.Value >= maxTimeCompressed ? true : false;
	}
}
