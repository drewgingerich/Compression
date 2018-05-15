using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	bool delay;

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckImpactTransition(state))
			return new ImpactController();
		if (CheckAirjumpTransition(state))
			return new AirjumpController();
		return null;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Airborn;
		state.gravityRatio.Value = 1f;
		state.impactMagnitude.Value = 0f;
		delay = true;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Airborn;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		if (delay)
			delay = false;
	}

	bool CheckImpactTransition(BallState state) {
		return state.grounded.Value && !delay;
	}

	bool CheckAirjumpTransition(BallState state) {
		return state.inputDirection.Value != Vector2.zero && !delay && state.airjumpAvailable.Value && state.freshInput.Value;
	}
}
