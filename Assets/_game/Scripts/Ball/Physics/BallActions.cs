using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BallActions {

	public static void LaunchBall(BallState state, Rigidbody2D rb2d) {
		// Vector2 launchDirection = -state.aimDirection.Value;
		// launchDirection = launchDirection.SnapRotation(32, Vector2.right);
		// float launchDistance = 2f;
		// float launchForce = Mathf.Sqrt(2 * state.gravityMagnitude.Value * launchDistance);
		// Vector2 launchVector = launchDirection * launchForce;
		// rb2d.AddForce(launchVector, ForceMode2D.Impulse);

		float maxDistance = 2.6f;
		float minDistance = 1.2f;
		float boostDistance = 0.7f;
		float limitDistance = 3.5f;
		float distance = Mathf.Pow(state.impactMagnitude.Value, 2) / (state.gravityMagnitude.Value * 2);
		Debug.Log(distance);
		if (distance == 0)
			distance = minDistance;
		else if (distance < maxDistance - boostDistance)
			distance += boostDistance;
		else if (distance < maxDistance)
			distance = maxDistance;
		else if (distance > limitDistance)
			distance = limitDistance + (distance - limitDistance) * 0.5f;
		Debug.Log(distance);
		float launchForce = Mathf.Sqrt(2 * state.gravityMagnitude.Value * distance);
		Vector2 launchVector = -state.aimDirection.Value * launchForce;
		// Debug.Log(state.impactMagnitude.Value.ToString() + " " + launchVector.magnitude.ToString());
		rb2d.AddForce(launchVector, ForceMode2D.Impulse);
		// rb2d.velocity = launchVector;
	}

	public static void SetAim(BallState state, Rigidbody2D rb2d) {
		state.aimDirection.Value = state.inputDirection.Value.ClampRotation(-state.contactNormal.Value, state.maxLaunchAngle.Value);
	}

	public static void Aim(BallState state, Rigidbody2D rb2d) {
		float maxAngleChange = state.aimSpeed.Value * Time.fixedDeltaTime / Mathf.Pow(state.timeInState.Value + 1, 0.3f);
		Vector2 aimDirection = state.inputDirection.Value.ClampRotation(state.aimDirection.Value, maxAngleChange);
		state.aimDirection.Value = aimDirection.ClampRotation(-state.contactNormal.Value, state.maxLaunchAngle.Value);
	}

	public static void ApplyGravity(BallState state, Rigidbody2D rb2d) {
		if (!state.gravity.Value)
			return;
		// if (rb2d.velocity.y <= -state.maxFallSpeed.Value)
		// 	return;
		rb2d.AddForce(state.gravityDirection.Value * state.gravityMagnitude.Value);
	}

	public static void ApplyFriction(BallState state, Rigidbody2D rb2d) {
		if (!state.friction.Value)
			return;
		float alignmentWithGravity = Vector2.Dot(rb2d.velocity.normalized, Physics2D.gravity.normalized);
		rb2d.AddForce(rb2d.velocity * -state.frictionMagnitude.Value * 4 * (alignmentWithGravity * -0.5f + 1));
	}

	public static void ApplyStickiness(BallState state, Rigidbody2D rb2d) {
		if (!state.sticky.Value)
			return;
		Vector2 perp = state.contactNormal.Value.Rotate(90f);
		float projection = Vector2.Dot(perp, rb2d.velocity.normalized);
		float force = projection * rb2d.velocity.magnitude;
		rb2d.velocity = perp * force;
		rb2d.AddForce(-state.contactNormal.Value * state.stickyMagnitude.Value);
	}
}
