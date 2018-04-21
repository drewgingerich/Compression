using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedController : BallController {
	
	float maxTimeCompressed = 0.5f;
	bool inAirLag;
	float airLag = 0f;
	float maxAirLag = 0.25f;
	float maxLaunchAngle = 65f;
	float maxAngularVelocity = 50f;

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		state.stateName.Value = StateName.Compressed;
		state.compressionDirection.Value = ClampDirection(state.inputDirection.Value, -state.contactNormal.Value, maxLaunchAngle);
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
		Vector2 clampedDirection = ClampDirection(state.inputDirection.Value, -state.contactNormal.Value, maxLaunchAngle);
		state.compressionDirection.Value = ClampDirection(clampedDirection, state.compressionDirection.Value, maxAngularVelocity * Time.deltaTime);
	}

	Vector2 ClampDirection(Vector2 direction, Vector2 referenceDirection, float maxAngle) {
		float angle = Vector2.SignedAngle(referenceDirection, direction);
		if (Mathf.Abs(angle) <= maxAngle)
			return direction;
		float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
		Quaternion rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
		return rotation * referenceDirection;
	}

	void LaunchBall(Rigidbody2D rb2d, Vector2 launchDirection) {
		float forceScaling = 6f;
		Vector2 releaseForce = launchDirection * forceScaling;
		rb2d.AddForce(releaseForce, ForceMode2D.Impulse);
	}

	bool CheckAirbornTransition(BallState state) {
		if (state.grounded.Value && !inAirLag)
			return false;
		if (!inAirLag) {
			inAirLag = true;
			return false;
		}
		airLag += Time.deltaTime;
		return airLag >= maxAirLag ? true : false;
	}

	bool CheckLaunchTransition(BallState state) {
		return state.inputDirection.Value == Vector2.zero ? true : false;
	}
}
