using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedController : BallController {

	protected float maxAirTime;
	protected float maxLaunchAngle;
	protected float maxAngularVelocity;

	public CompressedController() {
		maxAirTime = 0.1f;
		maxLaunchAngle = 65f;
		maxAngularVelocity = 50f;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Compressed;
		state.compressionDirection.Value = state.inputDirection.Value.Clamp(-state.contactNormal.Value, maxLaunchAngle);
		state.impactMagnitude.Value = 0f;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.impactMagnitude.Value = 0f;
		state.previousState.Value = StateName.Compressed;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckLaunchTransition(state)) {
			LaunchBall(state, rb2d, -state.compressionDirection.Value);
			return new AirbornController();
		}
		return null;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		Vector2 clampedDirection = state.inputDirection.Value.Clamp(-state.contactNormal.Value, maxLaunchAngle);
		float maxAngleChange = maxAngularVelocity * Time.deltaTime / Mathf.Sqrt(state.timeInState.Value);
		state.compressionDirection.Value = clampedDirection.Clamp(state.compressionDirection.Value, maxAngleChange);
	}

	protected void LaunchBall(BallState state, Rigidbody2D rb2d, Vector2 launchDirection) {
		float angle = Vector2.Angle(state.reboundDirection.Value, launchDirection);
		float reboundBoost = 0;
		if (angle <= 30) {
			reboundBoost = 0.4f * state.impactMagnitude.Value;
		} else {
			reboundBoost = 0.4f * state.impactMagnitude.Value;
		}
		float launchForce = 6 + reboundBoost;
		Vector2 launchVector = launchDirection * launchForce;
		rb2d.AddForce(launchVector, ForceMode2D.Impulse);
	}

	protected bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value && state.timeAirborn.Value >= maxAirTime ? true : false;
	}

	protected bool CheckLaunchTransition(BallState state) {
		return state.inputDirection.Value == Vector2.zero ? true : false;
	}
}
