using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompressedControllerInput;

public class CompressedController : BallController {
	
	protected float timeCompressed;
	protected float maxTimeCompressed = 0.5f;
	protected float maxLaunchAngle = 50f;
	protected Vector2 releaseVector;

	public override void Enter(Ball ball) {
		timeCompressed = 0f;
		ball.aimBar.Show();
		ball.animator.SetBool("Squished", true);
	}

	public override void Exit(Ball ball) {
		ball.aimBar.Hide();
		ball.animator.SetBool("Squished", false);
	}

	public override BallController CheckTransitions(Ball ball) {
		if (!ball.collisionManager.ballIsGrounded) {
			return new AirbornController();
		}
		Vector2 inputDirection = ball.inputScheme.GetInputDirection();
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

	// public override void Update(Ball ball) {
	// 	ball.collisionManager.timeGrounded += Time.deltaTime;
	// }

	// protected Vector2 GetLaunchReferenceVector(Ball ball, float reboundTimeout) {
	// 	Vector2 collisionNormal = ball.collisionManager.GetSumContactNormal();
	// 	Vector2 reflectionVector = ball.collisionManager.GetReboundVector();
	// 	Vector2 referenceVector;
	// 	if (ball.collisionManager.timeGrounded >= reboundTimeout) {
	// 		referenceVector = collisionNormal;
	// 	} else {
	// 		float scaleOne = ball.collisionManager.timeGrounded / reboundTimeout;
	// 		float scaleTwo = 1 - scaleOne;
	// 		referenceVector = scaleOne * collisionNormal + scaleTwo * reflectionVector;
	// 	}
	// 	return referenceVector;
	// }

	protected Vector2 ClampDirection(Vector2 direction, Vector2 referenceDirection) {
		float angle = Vector2.SignedAngle(referenceDirection, direction);
		if (Mathf.Abs(angle) <= maxLaunchAngle)
			return direction;
		float clampedAngle = Mathf.Clamp(angle, -maxLaunchAngle, maxLaunchAngle);
		Quaternion rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
		// Debug.Log(clampedAngle);
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
		// float stickyScaling = ball.sticky ? 250 : 150;
		float stickyScaling = 450f;
		Vector2 releaseForce = launchDirection * stickyScaling;
		ball.collisionManager.gameObject.GetComponent<Rigidbody2D>().AddForce(releaseForce);
	}
}
