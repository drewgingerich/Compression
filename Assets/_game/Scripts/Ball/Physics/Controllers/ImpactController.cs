using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : BallController {

	float timeInState = 0f;
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
		timeInState = 0f;
		state.gravityRatio.Value = 0f;
		rb2d.velocity = rb2d.velocity * 0.5f;
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
		timeInState += Time.deltaTime;
		return timeInState >= maxStickyTime ? true : false;
	}
}

