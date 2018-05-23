using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : BallController {

	float maxStickyTime = 0.5f;

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckReboundTransition(state))
			return new ReboundController();
		if (CheckGroundedTransition(state))
			return new GroundedController();
		return null;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Impact;
		state.gravityRatio.Value = 0f;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Impact;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		float alignmentWithGravity = Vector2.Dot(rb2d.velocity.normalized, Physics2D.gravity.normalized);
		Debug.Log(alignmentWithGravity);
		rb2d.AddForce(rb2d.velocity * -state.friction.Value * (alignmentWithGravity * -0.5f + 1));
	}
	
	bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value ? true : false;
	}

	bool CheckReboundTransition(BallState state) {
		return state.inputDirection.Value != Vector2.zero && state.freshInput.Value ? true : false;
	}

	bool CheckGroundedTransition(BallState state) {
		return state.timeInState.Value >= maxStickyTime ? true : false;
	}
}

