using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : BallController {

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckCompressedTransition(state))
			return CompressedControllerFactory.GetCompressedController(state.inputType.Value);
		return null;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.gravityRatio.Value = 1f;
	}

	bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value ? true : false;
	}

	bool CheckCompressedTransition(BallState state) {
		if (state.inputDirection.Value == Vector2.zero)
			return false;
		return Vector2.Dot(state.inputDirection.Value, state.contactNormal.Value) <= 0 ? true : false;
	}
}


