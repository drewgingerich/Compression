using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCompressedController : BallController {

	protected float timeCompressed;
	protected float maxTimeCompressed = 0.5f;
	protected bool inAirLag;
	protected float airLag = 0f;
	protected float maxAirLag = 0.25f;
	protected float maxLaunchAngle = 65f;
	protected Vector2 releaseVector;
	float maxAngularVelocity = 50f;
	Vector2 lastDirection;

	public override void Enter(Ball ball) {
		// Debug.Log("StickyCompressed State");
		timeCompressed = 0f;
		ball.aimBar.Show();
		ball.animator.SetBool("Squished", true);
		ball.state.CurrentGravity = 0f;
		lastDirection = ball.playerInfo.inputScheme.GetInputDirection();
		lastDirection = ClampDirection(lastDirection, -ball.state.ContactNormal, maxLaunchAngle);
		ball.rb2d.velocity = ball.rb2d.velocity * 0.5f;
		ball.spriteRenderer.color = Color.red;
	}

	public override void Exit(Ball ball) {
		ball.state.ImpactMagnitude = 0f;
		ball.aimBar.Hide();
		ball.animator.SetBool("Squished", false);
	}

	public override BallController CheckTransitions(Ball ball) {
		if (CheckAirbornTransition(ball))
			return new AirbornController();
		if (CheckLaunchTransition(ball) || CheckTimeoutTransition(ball)) {
			LaunchBall(ball, releaseVector);
			return new AirbornController();
		}
		return null;
	}

	public override void Update(Ball ball) {
		timeCompressed += Time.deltaTime;
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		Vector2 clampedDirection = ClampDirection(inputDirection, -ball.state.ContactNormal, maxLaunchAngle);
		Vector2 smoothedDirection = ClampDirection(clampedDirection, lastDirection, maxAngularVelocity * Time.deltaTime);
		lastDirection = smoothedDirection;
		float magnitude = FindMagnitude(smoothedDirection, ball.state.ReboundDirection);
		releaseVector = -smoothedDirection;
		ball.aimBar.UpdatePosition(-releaseVector);
	}

	protected Vector2 ClampDirection(Vector2 direction, Vector2 referenceDirection, float maxAngle) {
		float angle = Vector2.SignedAngle(referenceDirection, direction);
		if (Mathf.Abs(angle) <= maxAngle)
			return direction;
		float clampedAngle = Mathf.Clamp(angle, -maxAngle, maxAngle);
		Quaternion rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
		return rotation * referenceDirection;
	}

	protected float FindMagnitude(Vector2 direction, Vector2 referenceVector) {
		return referenceVector.magnitude;
	}

	protected Vector2 FindStartingInputDirection(Ball ball) {
		Vector2 contactNormalReference = -ball.state.ContactNormal;
		Vector2 reboundDirectionReference = -ball.state.ReboundDirection;
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		inputDirection = ClampDirection(inputDirection, contactNormalReference, 45f);
		if (Vector2.Angle(reboundDirectionReference, inputDirection) < 15f)
			inputDirection = reboundDirectionReference;
		return inputDirection;
	}

	protected void LaunchBall(Ball ball, Vector2 launchDirection) {
		float angle = Vector2.Angle(ball.state.ReboundDirection, launchDirection);
		float launchForce = 6;
		Debug.Log(ball.state.ImpactMagnitude);
		Debug.Log(launchDirection);
		if (angle <= 30) {
			float reboundBoost = 5f * 0.1f * ball.state.ImpactMagnitude;
			launchForce += reboundBoost;
		} else {
			float reboundBoost = 5f * 0.07f * ball.state.ImpactMagnitude;
			launchForce += reboundBoost;
		}
		Vector2 launchVector = launchDirection * launchForce;
		ball.rb2d.AddForce(launchVector, ForceMode2D.Impulse);
	}

	protected bool CheckAirbornTransition(Ball ball) {
		if (ball.state.Grounded && !inAirLag)
			return false;
		if (!inAirLag) {
			inAirLag = true;
			return false;
		}
		airLag += Time.deltaTime;
		return airLag >= maxAirLag ? true : false;
	}

	protected bool CheckLaunchTransition(Ball ball) {
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		return inputDirection == Vector2.zero ? true : false;
	}

	protected bool CheckTimeoutTransition(Ball ball) {
		return timeCompressed >= maxTimeCompressed ? true : false;
	}
}
