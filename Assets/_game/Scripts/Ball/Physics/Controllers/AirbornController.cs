using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	float lag = 0.2f;

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckImpactTransition(state))
			return new ImpactController();
		return null;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Airborn;
		state.gravityRatio.Value = 1f;
	}

	bool CheckImpactTransition(BallState state) {
		return state.grounded.Value && state.timeInState.Value >= lag;
	}
}
