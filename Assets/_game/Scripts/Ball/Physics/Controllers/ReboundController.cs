using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundController : CompressedController {

	float maxTimeCompressed;

	public ReboundController() : base() {
		maxTimeCompressed = 0.5f;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Rebound;
		state.compressionDirection.Value = state.inputDirection.Value.Clamp(-state.contactNormal.Value, maxLaunchAngle);
		state.gravityRatio.Value = 0f;
		rb2d.velocity = rb2d.velocity * 0.25f;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckLaunchTransition(state) || CheckTimeoutTransition(state)) {
			LaunchBall(state, rb2d, -state.compressionDirection.Value);
			return new AirbornController();
		}
		return null;
	}

	void LaunchBall(BallState state, Rigidbody2D rb2d, Vector2 launchDirection) {
		float angle = Vector2.Angle(state.reboundDirection.Value, launchDirection);
		float launchForce = 6;
		if (angle <= 30) {
			float reboundBoost = 5f * 0.1f * state.impactMagnitude.Value;
			launchForce += reboundBoost;
		} else {
			float reboundBoost = 5f * 0.07f * state.impactMagnitude.Value;
			launchForce += reboundBoost;
		}
		Vector2 launchVector = launchDirection * launchForce;
		rb2d.AddForce(launchVector, ForceMode2D.Impulse);
	}

	bool CheckTimeoutTransition(BallState state) {
		return state.timeInState.Value >= maxTimeCompressed ? true : false;
	}
}
