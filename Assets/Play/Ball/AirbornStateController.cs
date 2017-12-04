using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornStateController : BallStateController {

	BallBehavior ballBehavior;

	public AirbornStateController (BallBehavior ballBehavior) {
		this.ballBehavior = ballBehavior;
	}

	public override BallStateController CheckTransitions() {
		if (ballBehavior.collisions.Count > 0)
			return new GroundedStateController (ballBehavior);
		return null;
	}

	public override void Update () {
		Vector2 inputDirection = ballBehavior.controlScheme.GetInputDirection ();
		ballBehavior.gameObject.GetComponent<Rigidbody2D> ().AddForce (inputDirection * ballBehavior.speed);
	}
}
