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
		timeInState = 0f;
		Rigidbody2D rb2d = ball.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 0f;
		rb2d.velocity = rb2d.velocity * 0.5f;
	}
	
	bool CheckAirbornTransition(Ball ball) {
		return !ball.collisionManager.ballIsGrounded ? true : false;
	}

	bool CheckStickyCompressedTransition(Ball ball) {
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		Vector2 surfaceNormal = ball.collisionManager.GetSumContactNormal();
		return Vector2.Dot(inputDirection, surfaceNormal) < 0 ? true : false;
	}

	bool CheckGroundedTransition(Ball ball) {
		timeInState += Time.deltaTime;
		return timeInState >= maxStickyTime ? true : false;
	}
}

