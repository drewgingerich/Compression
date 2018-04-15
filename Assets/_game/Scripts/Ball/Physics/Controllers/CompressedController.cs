using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedController : BallController {
	
	protected float timeCompressed;
	protected float maxTimeCompressed = 0.5f;
	protected bool inAirLag;
	protected float airLag = 0f;
	protected float maxAirLag = 0.25f;
	protected float maxLaunchAngle = 65f;
	protected Vector2 compressionDirection;
	float maxAngularVelocity = 50f;

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		compressionDirection = ClampDirection(state.inputDirection.Value, -state.contactNormal.Value, maxLaunchAngle);
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.impactInfo.Value.magnitude = 0f;
	}

	public override BallController CheckTransitions(BallState ball, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(ball))
			return new AirbornController();
		if (CheckLaunchTransition(ball)) {
			LaunchBall(rb2d, -compressionDirection);
			return new AirbornController();
		}
		return null;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		Vector2 clampedDirection = ClampDirection(state.inputDirection.Value, -state.contactNormal.Value, maxLaunchAngle);
		compressionDirection = ClampDirection(clampedDirection, compressionDirection, maxAngularVelocity * Time.deltaTime);
	}

	protected Vector2 ClampDirection(Vector2 direction, Vector2 referenceDirection, float maxAngle) {
		float angle = Vector2.SignedAngle(referenceDirection, direction);
		if (Mathf.Abs(angle) <= maxAngle)
			return direction;
		float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
		Quaternion rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
		return rotation * referenceDirection;
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
