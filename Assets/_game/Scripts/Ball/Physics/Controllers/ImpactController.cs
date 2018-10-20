using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : BallController {

	float maxStickyTime = 0.5f;

	public override void Update(BallState state, Rigidbody2D rb2d) {
		BallActions.ApplyFriction(state, rb2d);
		BallActions.ApplyStickiness(state, rb2d);
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Impact;
		state.frictionMagnitude.Value = 2f;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Impact;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckReboundTransition(state))
			return new ReboundController();
		if (CheckGroundedTransition(state))
			return new GroundedController();
		return null;
	}

	bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value;
	}

	bool CheckReboundTransition(BallState state) {
		return state.inputDirection.Value != Vector2.zero && Vector2.Angle(state.inputDirection.Value, state.contactNormal.Value) > 50 && state.freshInput.Value;
	}

	bool CheckGroundedTransition(BallState state) {
		return state.timeInState.Value >= maxStickyTime;
	}
}

