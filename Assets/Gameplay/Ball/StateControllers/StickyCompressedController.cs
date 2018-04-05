using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCompressedController : BallController {

	protected float timeCompressed;
	protected float maxTimeCompressed = 0.5f;
	protected bool inAirLag;
	protected float airLag = 0f;
	protected float maxAirLag = 0.25f;
	protected float maxLaunchAngle = 85f;
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
		Vector2 referenceVector = ball.state.ReboundDirection;
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		Vector2 clampedDirection = ClampDirection(inputDirection, -referenceVector, maxLaunchAngle);
		Vector2 smoothedDirection = ClampDirection(clampedDirection, lastDirection, maxAngularVelocity * Time.deltaTime);
		lastDirection = smoothedDirection;
		float magnitude = FindMagnitude(smoothedDirection, referenceVector);
		releaseVector = -smoothedDirection * magnitude;
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
		float launchForce = 350;
		Debug.Log(string.Format("ReboundDirection: {0}, LaunchDirection: {1}, Angle: {2}", ball.state.ReboundDirection, launchDirection, angle));
		if (angle <= 30) {
			float reboundScaling = 1.7f - 2f / (1f + ball.state.ImpactMagnitude);
			launchForce *= reboundScaling;
		}
		// if (angle <= 30) {
		// 	float reboundBoost = 100 * 0.2f * ball.state.ImpactMagnitude;
		// 	launchForce += reboundBoost;
		// }
		Vector2 launchVector = launchDirection * launchForce;
		ball.collisionManager.gameObject.GetComponent<Rigidbody2D>().AddForce(launchVector);
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
