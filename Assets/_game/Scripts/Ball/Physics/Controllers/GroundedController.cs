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
		state.stateName.Value = StateName.Grounded;
		state.gravityRatio.Value = 1f;
	}

	bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value ? true : false;
	}

	bool CheckCompressedTransition(BallState state) {
		return state.inputDirection.Value != Vector2.zero ? true : false;
	}
}


