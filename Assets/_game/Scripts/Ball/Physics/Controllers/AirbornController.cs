using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	float timeInState;
	float lag = 0.2f;

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckStickyGroundedTransition(state))
			return new ImpactController();
		return null;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.currentGravity.Value = state.baseGravity.Value;
		timeInState = 0f;
	}

	bool CheckStickyGroundedTransition(BallState state) {
		timeInState += Time.deltaTime;
		return state.grounded.Value && timeInState >= lag;
	}
}
