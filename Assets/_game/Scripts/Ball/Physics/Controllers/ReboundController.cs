using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundController : BallController {

	protected float timeCompressed;
	protected float maxTimeCompressed = 0.5f;
	protected bool inAirLag;
	protected float airLag = 0f;
	protected float maxAirLag = 0.25f;
	protected float maxLaunchAngle = 65f;
	protected Vector2 releaseVector;
	float maxAngularVelocity = 50f;
	Vector2 lastDirection;

	public override void Enter(BallState state, Rigidbody2D rb2d) {
		// Debug.Log("StickyCompressed State");
		timeCompressed = 0f;
		// ball.aimBar.Show();
		// ball.animator.SetBool("Squished", true);
		state.currentGravity.Value = 0f;
		lastDirection = state.inputDirection.Value;
		lastDirection = ClampDirection(lastDirection, state.contactNormal.Value, maxLaunchAngle);
		rb2d.velocity = rb2d.velocity * 0.5f;
		// ball.spriteRenderer.color = Color.red;
	}

	public override void Exit(BallState state, Rigidbody2D rb2d) {
		state.impactInfo.Value.magnitude = 0f;
		// ball.aimBar.Hide();
		// ball.animator.SetBool("Squished", false);
	}

	public override BallController CheckTransitions(BallState state, Rigidbody2D rb2d) {
		if (CheckAirbornTransition(state))
			return new AirbornController();
		if (CheckLaunchTransition(state) || CheckTimeoutTransition(state)) {
			LaunchBall(state, rb2d, releaseVector);
			return new AirbornController();
		}
		return null;
	}

	public override void Update(BallState state, Rigidbody2D rb2d) {
		timeCompressed += Time.deltaTime;
		Vector2 inputDirection = state.inputDirection.Value;
		Vector2 clampedDirection = ClampDirection(inputDirection, state.contactNormal.Value, maxLaunchAngle);
		Vector2 smoothedDirection = ClampDirection(clampedDirection, lastDirection, maxAngularVelocity * Time.deltaTime);
		lastDirection = smoothedDirection;
		releaseVector = -smoothedDirection;
		// ball.aimBar.UpdatePosition(-releaseVector);
	}

	protected Vector2 ClampDirection(Vector2 direction, Vector2 referenceDirection, float maxAngle) {
		float angle = Vector2.SignedAngle(referenceDirection, direction);
		if (Mathf.Abs(angle) <= maxAngle)
			return direction;
		float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
		Quaternion rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
		return rotation * referenceDirection;
	}

	protected Vector2 FindStartingInputDirection(BallState state, Rigidbody2D rb2d) {
		Vector2 contactNormalReference = state.contactNormal.Value;
		Vector2 reboundDirectionReference = state.impactInfo.Value.reboundDirection;
		Vector2 inputDirection = state.inputDirection.Value;
		inputDirection = ClampDirection(inputDirection, contactNormalReference, 45f);
		if (Vector2.Angle(reboundDirectionReference, inputDirection) < 15f)
			inputDirection = reboundDirectionReference;
		return inputDirection;
	}

	protected void LaunchBall(BallState state, Rigidbody2D rb2d, Vector2 launchDirection) {
		float angle = Vector2.Angle(state.impactInfo.Value.reboundDirection, launchDirection);
		float launchForce = 6;
		if (angle <= 30) {
			float reboundBoost = 5f * 0.1f * state.impactInfo.Value.magnitude;
			launchForce += reboundBoost;
		} else {
			float reboundBoost = 5f * 0.07f * state.impactInfo.Value.magnitude;
			launchForce += reboundBoost;
		}
		Vector2 launchVector = launchDirection * launchForce;
		rb2d.AddForce(launchVector, ForceMode2D.Impulse);
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

	protected bool CheckTimeoutTransition(BallState state) {
		return timeCompressed >= maxTimeCompressed ? true : false;
	}
}
