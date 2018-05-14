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
		float maxDistance = 2f;
		float minDistance = 0.75f;
		float boostDistance = 0.5f;
		float limitDistance = 3f;
		float distance = Mathf.Pow(state.impactMagnitude.Value, 2) / (Physics2D.gravity.magnitude * 2);
		if (distance == 0)
			distance = minDistance;
		else if (distance < maxDistance - boostDistance)
			distance += boostDistance;
		else if (distance < maxDistance)
			distance = maxDistance;
		else if (distance > limitDistance)
			distance = limitDistance + Mathf.Pow(distance - limitDistance, 0.6f);
		float launchForce = Mathf.Pow(2 * Physics2D.gravity.magnitude * distance, 0.5f);
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
