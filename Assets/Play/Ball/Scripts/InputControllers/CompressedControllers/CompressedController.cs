﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompressedControllerInput;

public class CompressedController : BallController {

	float timeCompressed = 0;
	float reflectionDieOff = 1;
	IInputHandler inputHandler;
	Ball ball;
	BallCollisionManager collisionManager;
	BallState ballState;
	Vector2 releaseVector;
	// float maxStickyTime = 0.5f;

	public CompressedController(Ball ball) {
		this.ball = ball;
		inputHandler = InputHandlerFactory.GetInputHandler(ball.inputScheme);
		collisionManager = ball.collisionManager;
		ballState  = ball.state;
	}

	public override BallController CheckTransitions() {
		if (!ballState.grounded)
			return new AirbornController(ball);
		Vector2 inputDirection = ball.inputScheme.GetInputDirection();
		Vector2 sumNormal = collisionManager.GetReflectionVector();
		// if (Vector2.Dot(inputDirection, sumNormal) >= 0) {;
		// 	return new AirbornController(ball);
		// }
		if (inputDirection == Vector2.zero)
			return new AirbornController(ball);
		return null;
	}

	public override void Update() {
		timeCompressed += Time.deltaTime;
		ball.state.timeGrounded += Time.deltaTime;
		Vector2 collisionNormal = collisionManager.GetSumContactNormal();
		Vector2 reflectionVector = collisionManager.GetReflectionVector();
		Vector2 referenceVector;
		if (timeCompressed >= reflectionDieOff) {
			referenceVector = collisionNormal;
		} else {
			float scaleOne = ball.state.timeGrounded / reflectionDieOff;
			float scaleTwo = 1 - scaleOne;
			referenceVector = scaleOne * collisionNormal + scaleTwo * reflectionVector;
		}
		Vector2 inputVector = inputHandler.GetInputVector(referenceVector.normalized);
		float magnitude = Mathf.Sqrt(Mathf.Abs(Vector2.Dot(inputVector, referenceVector)));
		releaseVector = -inputVector * magnitude;
		ball.aimBar.UpdatePosition(-releaseVector);
		// if (ball.sticky) {
		// 	ball.stickyTime += Time.deltaTime;
		// 	if (ball.stickyTime >= maxStickyTime)
		// 		ball.DisableSticky ();
		// }
	}

	public override void Enter() {
		ball.aimBar.Show();
		// if (!ball.sticky)
		// 	ball.EnableSticky ();
		ball.animator.SetBool ("Squished", true);
	}

	public override void Exit() {
		LaunchBall();
		ball.aimBar.Hide();
		// if (ball.sticky)
		// 	ball.DisableSticky ();
		ball.animator.SetBool ("Squished", false);
	}

	void LaunchBall() {
		// float stickyScaling = ball.sticky ? 250 : 150;
		float stickyScaling = 350;
		Vector2 releaseForce = releaseVector * stickyScaling;
		ball.collisionManager.gameObject.GetComponent<Rigidbody2D>().AddForce(releaseForce);
	}
}
