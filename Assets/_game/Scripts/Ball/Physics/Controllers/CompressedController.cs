using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedController : BallController {

	protected float maxAirTime = 0.1f;
	protected float maxLaunchAngle;
	protected float maxAngularVelocity;

	public override void Update(BallState state, Rigidbody2D rb2d) {
		BallActions.ApplyGravity(state, rb2d);
		BallActions.ApplyFriction(state, rb2d);
		BallActions.Aim(state, rb2d);
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Compressed;
		state.impactMagnitude.Value = 0f;
		state.frictionMagnitude.Value = 10f;
		BallActions.SetAim(state, rb2d);
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.impactMagnitude.Value = 0f;
		state.previousState.Value = StateName.Compressed;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckLaunchTransition(state)) {
			state.freshInput.Value = false;
			BallActions.LaunchBall(state, rb2d);
			return new AirbornController();
		}
		return null;
	}

	protected bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value && state.timeAirborn.Value >= maxAirTime;
	}

	protected bool CheckLaunchTransition(BallState state) {
		return state.inputDirection.Value == Vector2.zero;
	}
}
