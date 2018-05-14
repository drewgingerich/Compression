using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchedController : BallController {

	float stateTime = 0.1f;

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.gravityRatio.Value = 0f;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckTimeoutTransition(state))
			return new AirbornController();
		return null;
	}

	bool CheckTimeoutTransition(BallState state) {
		return state.timeInState.Value >= stateTime ? true : false;
	}
}
