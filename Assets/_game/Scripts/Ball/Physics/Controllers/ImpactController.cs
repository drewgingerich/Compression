using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : BallController {

	float maxStickyTime = 0.5f;

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckStickyCompressedTransition(state))
			return new ReboundController();
		if (CheckGroundedTransition(state))
			return new GroundedController();
		return null;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Impact;
		state.gravityRatio.Value = 0f;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		rb2d.AddForce(rb2d.velocity * -1.5f);
	}
	
	bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value ? true : false;
	}

	bool CheckStickyCompressedTransition(BallState state) {
		if (state.inputDirection.Value == Vector2.zero)
			return false;
		return Vector2.Dot(state.inputDirection.Value, state.contactNormal.Value) <= 0 ? true : false;
	}

	bool CheckGroundedTransition(BallState state) {
		return state.timeInState.Value >= maxStickyTime ? true : false;
	}
}

