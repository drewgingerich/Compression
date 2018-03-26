using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedController : BallController {

	float maxStickyTime = 0.25f;

	public override BallController CheckTransitions(Ball ball) {
		if (!ball.collisionManager.ballIsGrounded) {
			return new AirbornController();
		}
		Vector2 inputDirection = ball.playerInfo.inputScheme.GetInputDirection();
		Vector2 reboundVector = ball.collisionManager.GetReboundVector();
		if (Vector2.Dot(inputDirection, reboundVector) < 0) {
			return CompressedControllerFactory.GetCompressedController(ball.playerInfo.inputScheme); 
		}
		return null;
	}

	public override void Enter(Ball ball) {
		ball.collisionManager.timeGrounded = 0f;
		ball.stickyManager.ballIsSticky = true;
		Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 0f;
		rb2d.velocity = rb2d.velocity * 0.5f;
	}

	public override void Exit(Ball ball) {
		Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
		rb2d.gravityScale = 1f;
	}

	public override void Update(Ball ball) {
		ball.collisionManager.timeGrounded += Time.deltaTime;
		if (ball.collisionManager.timeGrounded >= maxStickyTime) {
			Rigidbody2D rb2d = ball.collisionManager.gameObject.GetComponent<Rigidbody2D>();
			rb2d.gravityScale = 1f;	
			ball.stickyManager.ballIsSticky = false;
		}
		// if (ball.collisionManager.timeGround
		// Vector2 inputDirection = ball.controlScheme.direction;
		// ball.gameObject.GetComponent<Rigidbody2D> ().AddForce (inputDirection);
		// if (ball.sticky) {
		// 	ball.stickyTime += Time.deltaTime;
		// 	if (ball.stickyTime >= maxStickyTime)
		// 		ball.DisableSticky ();
		// }
	}
}
