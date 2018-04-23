using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedController : BallController {

	protected bool inAirLag;
	protected float airLag;
	protected float maxAirLag;
	protected float maxLaunchAngle;
	protected float maxAngularVelocity;

	public CompressedController() {
		inAirLag = false;
		airLag = 0f;
		maxAirLag = 0.25f;
		maxLaunchAngle = 65f;
		maxAngularVelocity = 50f;
	}

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Compressed;
		state.compressionDirection.Value = state.inputDirection.Value.Clamp(-state.contactNormal.Value, maxLaunchAngle);
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.impactMagnitude.Value = 0f;
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckLaunchTransition(state)) {
			LaunchBall(rb2d, -state.compressionDirection.Value);
			return new AirbornController();
		}
		return null;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		Vector2 clampedDirection = state.inputDirection.Value.Clamp(-state.contactNormal.Value, maxLaunchAngle);
		state.compressionDirection.Value = clampedDirection.Clamp(state.compressionDirection.Value, maxAngularVelocity * Time.deltaTime);
	}

	protected void LaunchBall(Rigidbody2D rb2d, Vector2 launchDirection) {
		float forceScaling = 6f;
		Vector2 releaseForce = launchDirection * forceScaling;
		rb2d.AddForce(releaseForce, ForceMode2D.Impulse);
	}

	protected bool CheckAirbornTransition(BallState state) {
		if (state.grounded.Value && !inAirLag)
			return false;
		if (!inAirLag) {
			inAirLag = true;
			return false;
		}
		airLag += Time.deltaTime;
		return airLag >= maxAirLag ? true : false;
	}

	protected bool CheckLaunchTransition(BallState state) {
		return state.inputDirection.Value == Vector2.zero ? true : false;
	}
}
