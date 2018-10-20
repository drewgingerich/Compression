using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : BallController {

	public override void Update(BallState state, Rigidbody2D rb2d) {
		BallActions.ApplyGravity(state, rb2d);
		BallActions.ApplyFriction(state, rb2d);
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Grounded;
		state.frictionMagnitude.Value = 1f;
		// Vector2 perp = state.contactNormal.Value.Rotate(90f);
		// float projection = Vector2.Dot(perp, state.impactDirection.Value);
		// float force = projection * state.impactMagnitude.Value;
		// Debug.Log(state.impactDirection.Value);
		// rb2d.AddForce(perp * force, ForceMode2D.Impulse);
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.previousState.Value = StateName.Grounded;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckCompressedTransition(state))
			return new CompressedController();
		return null;
	}

	bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value;
	}

	bool CheckCompressedTransition(BallState state) {
		return state.inputDirection.Value != Vector2.zero && Vector2.Angle(state.inputDirection.Value, state.contactNormal.Value) > 50 && state.freshInput.Value;
	}
}
