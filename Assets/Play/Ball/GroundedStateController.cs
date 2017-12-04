﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedStateController : BallStateController {

	BallBehavior ballBehavior;
	float maxStickyTime = 0.3f;

	public GroundedStateController (BallBehavior ballBehavior) {
		this.ballBehavior = ballBehavior;
	}

	public override void Enter () {
		ballBehavior.EnableSticky ();
	}

	public override BallStateController CheckTransitions() {
		if (ballBehavior.collisions.Count == 0) {
			ballBehavior.DisableSticky ();
			return new AirbornStateController (ballBehavior);
		}
		Vector2 inputDirection = ballBehavior.controlScheme.GetInputDirection ();
		Vector2 sumNormal = ballBehavior.GetSumContactNormal ();
		if (Vector2.Dot (inputDirection, sumNormal) < 0)
			return new CompressedStateController (ballBehavior);
		return null;
	}

	public override void Update () {
		Vector2 inputDirection = ballBehavior.controlScheme.GetInputDirection ();
		ballBehavior.gameObject.GetComponent<Rigidbody2D> ().AddForce (inputDirection * ballBehavior.speed);
		if (ballBehavior.sticky) {
			ballBehavior.stickyTime += Time.deltaTime;
			if (ballBehavior.stickyTime >= maxStickyTime)
				ballBehavior.DisableSticky ();
		}
	}
}