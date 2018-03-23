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
	}

	public override void Exit(Ball ball){
		base.Exit(ball);
		Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 1f;
	}

	public override void Update(Ball ball){
		timeCompressed += Time.deltaTime;
		Vector2 referenceVector;
		if (ball.stickyManager.ballIsSticky)
			referenceVector = ball.collisionManager.GetReboundVector();
		else
			referenceVector = ball.collisionManager.GetSumContactNormal();
		Vector2 inputDirection = ball.inputScheme.GetInputDirection();
		Vector2 clampedDirection = ClampDirection(inputDirection, -referenceVector.normalized);
		Vector2 smoothedDirection = SmoothDirectionChange(clampedDirection, lastDirection, maxAngularVelocity);
		lastDirection = smoothedDirection;
		float magnitude = FindMagnitude(smoothedDirection, referenceVector);
		releaseVector = -smoothedDirection * magnitude;
		ball.aimBar.UpdatePosition(-releaseVector);
	}
}
