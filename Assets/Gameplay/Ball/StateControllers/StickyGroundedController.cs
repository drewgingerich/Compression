using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyGroundedController : BallController {

	float timeInState = 0f;
	float maxStickyTime = 0.25f;

	public override BallController CheckTransitions(Ball ball) {
		if (CheckAirbornTransition(ball))
			return new AirbornController();
		if (CheckStickyCompressedTransition(ball))
			return new StickyCompressedController();
		if (CheckGroundedTransition(ball))
			return new GroundedController();
		return null;
	}

	public override void Enter(Ball ball) {
		// Debug.Log("StickyGrounded State");
		timeInState = 0f;
		ball.state.CurrentGravity = 0f;
		ball.rb2d.velocity = ball.rb2d.velocity * 0.5f;
		ball.spriteRenderer.color = Color.yellow;
	}
	
	bool CheckAirbornTransition(Ball ball) {
		return !ball.state.Grounded ? true : false;
	}

	bool CheckStickyCompressedTransition(Ball ball) {
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		Vector2 contactNormal = ball.state.ContactNormal;
		return Vector2.Dot(inputDirection, contactNormal) < 0 ? true : false;
	}

	bool CheckGroundedTransition(Ball ball) {
		timeInState += Time.deltaTime;
		return timeInState >= maxStickyTime ? true : false;
	}
}

