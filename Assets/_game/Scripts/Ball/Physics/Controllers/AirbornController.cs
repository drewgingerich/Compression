using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	bool delay;
	int frameCount;

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckImpactTransition(state))
			return new ImpactController();
		return null;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Airborn;
		state.gravityRatio.Value = 1f;
		delay = true;
		frameCount = 0;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Airborn;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		rb2d.AddForce(state.inputDirection.Value * 2);
		frameCount++;
		if (frameCount > 1)
			delay = false;
	}

	bool CheckImpactTransition(BallState state) {
		return state.grounded.Value && !delay;
	}
}
