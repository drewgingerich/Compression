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
	protected Vector2 releaseVector;
	float maxAngularVelocity = 50f;
	Vector2 lastDirection;

	public override void Enter(Ball ball) {
		// Debug.Log("Compressed State");
		ball.aimBar.Show();
		ball.animator.SetBool("Squished", true);
		lastDirection = ball.playerInfo.inputScheme.GetInputDirection();
		ball.spriteRenderer.color = Color.blue;
	}

	public override void Exit(Ball ball) {
		ball.state.ImpactMagnitude = 0f;
		ball.aimBar.Hide();
		ball.animator.SetBool("Squished", false);
	}

	public override BallController CheckTransitions(Ball ball) {
		if (CheckAirbornTransition(ball))
			return new AirbornController();
		if (CheckLaunchTransition(ball)) {
			LaunchBall(ball, releaseVector);
			return new AirbornController();
		}
		return null;
	}

	public override void Update(Ball ball) {
		Vector2 referenceVector = ball.state.ContactNormal;
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		Vector2 clampedDirection = ClampDirection(inputDirection, -referenceVector.normalized, maxLaunchAngle);
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

	protected float FindMagnitude (Vector2 direction, Vector2 referenceVector) {
		return referenceVector.magnitude;
	}

	protected void LaunchBall(Ball ball, Vector2 launchDirection) {
		float forceScaling = 6f;
		Vector2 releaseForce = launchDirection * forceScaling;
		ball.rb2d.AddForce(releaseForce, ForceMode2D.Impulse);
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
}
