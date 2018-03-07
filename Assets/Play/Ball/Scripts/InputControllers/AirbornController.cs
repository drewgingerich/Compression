using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornController : BallController {

	Ball ball;

	public AirbornController (Ball ball) {
		this.ball = ball;
	}

	public override BallController CheckTransitions() {
		if (ball.state.grounded)
			return new GroundedController (ball);
		return null;
	}

	public override void Update () {
		// Vector2 inputDirection = ball.controlScheme.direction;
		// ball.gameObject.GetComponent<Rigidbody2D> ().AddForce (inputDirection * ball.speed);
	}
}
