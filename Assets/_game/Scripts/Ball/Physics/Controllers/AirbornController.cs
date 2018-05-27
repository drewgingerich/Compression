using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	int framesInState = 0;
	int minimumFramesInState = 3;

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckImpactTransition(state))
			return new ImpactController();
		return null;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Airborn;
		state.gravityRatio.Value = 1f;
		state.impactMagnitude.Value = 0f;
		framesInState = 0;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Airborn;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		framesInState += 1;
	}

	bool CheckImpactTransition(BallState state) {
		return state.grounded.Value && framesInState > minimumFramesInState;
	}

	bool CheckAirjumpTransition(BallState state) {
		return state.inputDirection.Value != Vector2.zero && framesInState > minimumFramesInState && state.freshInput.Value;
	}
}
