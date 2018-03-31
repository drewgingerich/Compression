using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : BallController {

	public override BallController CheckTransitions(Ball ball) {
		if (CheckAirbornTransition(ball))
			return new AirbornController();
		if (CheckCompressedTransition(ball))
			return CompressedControllerFactory.GetCompressedController(ball.playerInfo.inputScheme);
		return null;
	}

	public override void Enter(Ball ball) {
		ball.state.CurrentGravity = 1f;
	}

	bool CheckAirbornTransition(Ball ball) {
		return !ball.state.Grounded ? true : false;
	}

	bool CheckCompressedTransition(Ball ball) {
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		Vector2 contactNormal = ball.state.ContactNormal;
		return Vector2.Dot(inputDirection, contactNormal) < 0 ? true : false;
	}
}


