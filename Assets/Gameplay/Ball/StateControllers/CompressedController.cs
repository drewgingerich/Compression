using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedController : BallController {
	
	protected float timeCompressed;
	protected float maxTimeCompressed = 0.5f;
	protected float maxLaunchAngle = 50f;
	protected Vector2 releaseVector;
	float maxAngularVelocity = 50f;
	Vector2 lastDirection;

	public CompressedController() {

	}

	public override void Enter(Ball ball) {
		timeCompressed = 0f;
		ball.aimBar.Show();
		ball.animator.SetBool("Squished", true);
		Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 0;
		Vector2 referenceVector;
		if (ball.stickyManager.ballIsSticky)
			referenceVector = ball.collisionManager.GetReboundVector();
		else
			referenceVector = ball.collisionManager.GetSumContactNormal();
		lastDirection = -referenceVector;
	}

	public override void Exit(Ball ball) {
		ball.aimBar.Hide();
		ball.animator.SetBool("Squished", false);
		Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 1f;
	}

	public override BallController CheckTransitions(Ball ball) {
		if (!ball.collisionManager.ballIsGrounded) {
			return new AirbornController();
		}
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		if (inputDirection == Vector2.zero) {
			LaunchBall(ball, releaseVector);
			return new AirbornController();
		}
		if (ball.stickyManager.ballIsSticky && timeCompressed >= maxTimeCompressed) {
			ball.stickyManager.ballIsSticky = false;
			LaunchBall(ball, releaseVector);
			return new AirbornController();
		}
		return null;
	}

	public override void Update(Ball ball) {
		timeCompressed += Time.deltaTime;
		Vector2 referenceVector;
		if (ball.stickyManager.ballIsSticky)
			referenceVector = ball.collisionManager.GetReboundVector();
		else
			referenceVector = ball.collisionManager.GetSumContactNormal();
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		Vector2 clampedDirection = ClampDirection(inputDirection, -referenceVector.normalized);
		Vector2 smoothedDirection = SmoothDirectionChange(clampedDirection, lastDirection, maxAngularVelocity);
		lastDirection = smoothedDirection;
		float magnitude = FindMagnitude(smoothedDirection, referenceVector);
		releaseVector = -smoothedDirection * magnitude;
		ball.aimBar.UpdatePosition(-releaseVector);
	}

	protected Vector2 ClampDirection(Vector2 direction, Vector2 referenceDirection) {
		float angle = Vector2.SignedAngle(referenceDirection, direction);
		if (Mathf.Abs(angle) <= maxLaunchAngle)
			return direction;
		float clampedAngle = Mathf.Clamp(angle, -maxLaunchAngle, maxLaunchAngle);
		Quaternion rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
		return rotation * referenceDirection;
	}

	protected Vector2 SmoothDirectionChange(Vector2 direction, Vector2 previousDirection, float maxAngularVelocity) {
		if (previousDirection == Vector2.zero)
			return direction;
		float maxAngularMovement = maxAngularVelocity * Time.deltaTime;
		float angularMovement = Vector2.SignedAngle(previousDirection, direction);
		if (Mathf.Abs(angularMovement) <= maxAngularMovement)
			return direction;
		float smoothedMovement = Mathf.Clamp(angularMovement, -maxAngularMovement, maxAngularMovement);
		Quaternion rotation = Quaternion.AngleAxis(smoothedMovement, Vector3.forward);
		return rotation * previousDirection;
	}

	protected float FindMagnitude (Vector2 direction, Vector2 referenceVector) {
		return referenceVector.magnitude;
	}

	protected void LaunchBall(Ball ball, Vector2 launchDirection) {
		float forceScaling = 450f;
		Vector2 releaseForce = launchDirection * forceScaling;
		ball.collisionManager.gameObject.GetComponent<Rigidbody2D>().AddForce(releaseForce);
	}
}
