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
		maxAngularVelocity = 100f;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Compressed;
		state.compressionDirection.Value = state.inputDirection.Value.ClampRotation(-state.contactNormal.Value, maxLaunchAngle);
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
			BallActions.LaunchBall(state, rb2d, -state.compressionDirection.Value);
			state.freshInput.Value = false;
			return new AirbornController();
		}
		return null;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		Vector2 clampedDirection = state.inputDirection.Value.ClampRotation(-state.contactNormal.Value, maxLaunchAngle);
		float maxAngleChange = maxAngularVelocity * Time.deltaTime / Mathf.Pow(state.timeInState.Value + 1, 0.3f);
		state.compressionDirection.Value = clampedDirection.ClampRotation(state.compressionDirection.Value, maxAngleChange);
		float alignmentWithGravity = Vector2.Dot(rb2d.velocity.normalized, Physics2D.gravity.normalized);
		rb2d.AddForce(rb2d.velocity * -state.friction.Value * 4 * (alignmentWithGravity * -0.5f + 1));
	}

	protected bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value && state.timeAirborn.Value >= maxAirTime;
	}

	protected bool CheckLaunchTransition(BallState state) {
		return state.inputDirection.Value == Vector2.zero;
	}
}
