using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchedController : BallController {

	float stateTime = 0.1f;

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		// state.gravityRatio.Value = 0f;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckImpactTransition(state))
			return new ImpactController();
		if (CheckTimeoutTransition(state))
			return new AirbornController();
		return null;
	}

	bool CheckImpactTransition(BallState state) {
		return state.grounded.Value;
	}

	bool CheckTimeoutTransition(BallState state) {
		return state.timeInState.Value >= stateTime ? true : false;
	}
}
