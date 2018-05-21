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
			LaunchBall(state, rb2d, -state.compressionDirection.Value);
			state.freshInput.Value = false;
			return new AirbornController();
		}
		return null;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		Vector2 clampedDirection = state.inputDirection.Value.ClampRotation(-state.contactNormal.Value, maxLaunchAngle);
		float maxAngleChange = maxAngularVelocity * Time.deltaTime / Mathf.Sqrt(state.timeInState.Value);
		state.compressionDirection.Value = clampedDirection.ClampRotation(state.compressionDirection.Value, maxAngleChange);
	}

	protected void LaunchBall(BallState state, Rigidbody2D rb2d, Vector2 launchDirection) {
		float launchDistance = 2f;
		float launchForce = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * launchDistance);
		Vector2 launchVector = launchDirection * launchForce;
		rb2d.AddForce(launchVector, ForceMode2D.Impulse);
		// float maxDistance = 2.6f;
		// float minDistance = 1.2f;
		// float boostDistance = 0.7f;
		// float limitDistance = 3.5f;
		// float distance = Mathf.Pow(state.impactMagnitude.Value, 2) / (Physics2D.gravity.magnitude * 2);
		// if (distance == 0)
		// 	distance = minDistance;
		// else if (distance < maxDistance - boostDistance)
		// 	distance += boostDistance;
		// else if (distance < maxDistance)
		// 	distance = maxDistance;
		// else if (distance > limitDistance)
		// 	distance = limitDistance + (distance - limitDistance) * 0.5f;
		// // distance += 1.25f;
		// float launchForce = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * distance);
		// Vector2 launchVector = launchDirection * launchForce;
		// Debug.Log(state.impactMagnitude.Value.ToString() + " " + launchVector.magnitude.ToString());
		// rb2d.AddForce(launchVector, ForceMode2D.Impulse);
		// rb2d.velocity = launchVector;
	}

	protected bool CheckAirbornTransition(BallState state) {
		return !state.grounded.Value && state.timeAirborn.Value >= maxAirTime;
	}

	protected bool CheckLaunchTransition(BallState state) {
		return state.inputDirection.Value == Vector2.zero;
	}
}
