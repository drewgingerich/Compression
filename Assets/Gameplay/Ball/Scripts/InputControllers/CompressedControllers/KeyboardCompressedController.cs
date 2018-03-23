using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCompressedController : CompressedController {

	float reboundTimeout = 1f;
	float maxAngularVelocity = 50f;
	Vector2 lastDirection;

	public KeyboardCompressedController() {
		lastDirection = Vector2.zero;
	}

	public override void Enter(Ball ball){
		base.Enter(ball);
		Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 0;
		Vector2 referenceVector;
		if (ball.stickyManager.ballIsSticky)
			referenceVector = ball.collisionManager.GetReboundVector();
		else
			referenceVector = ball.collisionManager.GetSumContactNormal();
		lastDirection = -referenceVector;
		// rb2d.velocity = rb2d.velocity * 0.25f;
		// if (!ball.stickyManager.ballIsSticky)
		// 	ball.stickyManager.EnableSticky(ball);
	}

	public override void Exit(Ball ball){
		base.Exit(ball);
		Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 1f;
		// if (ball.stickyManager.ballIsSticky)
		// 	ball.stickyManager.DisableSticky(ball);
	}

	public override void Update(Ball ball){
		timeCompressed += Time.deltaTime;
		// ball.collisionManager.timeGrounded += Time.deltaTime;
		// if (ball.stickyManager.ballIsSticky)
		// 	if (ball.stickyManager.stickyTime >= stickyDuration)
		// 		ball.stickyManager.DisableSticky(ball);
		Vector2 referenceVector;
		if (ball.stickyManager.ballIsSticky)
			referenceVector = ball.collisionManager.GetReboundVector();
		else
			referenceVector = ball.collisionManager.GetSumContactNormal();
		// Debug.Log(referenceVector);
		Vector2 inputDirection = ball.inputScheme.GetInputDirection();
		Vector2 clampedDirection = ClampDirection(inputDirection, -referenceVector.normalized);
		Vector2 smoothedDirection = SmoothDirectionChange(clampedDirection, lastDirection, maxAngularVelocity);
		lastDirection = smoothedDirection;
		float magnitude = FindMagnitude(smoothedDirection, referenceVector);
		releaseVector = -smoothedDirection * magnitude;
		ball.aimBar.UpdatePosition(-releaseVector);
	}
}
