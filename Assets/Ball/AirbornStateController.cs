using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornStateController : BallStateController {

	BallBehavior ballBehavior;

	public AirbornStateController (BallBehavior ballBehavior) {
		this.ballBehavior = ballBehavior;
	}

	public override BallStateController CheckTransitions() {
		if (ballBehavior.grounded)
			return new GroundedStateController (ballBehavior);
		return null;
	}

	public override void Update () {
		Vector2 inputDirection = InputUtility.GetInputDirection ();
		ballBehavior.gameObject.GetComponent<Rigidbody2D> ().AddForce (inputDirection * ballBehavior.speed);
	}
}
