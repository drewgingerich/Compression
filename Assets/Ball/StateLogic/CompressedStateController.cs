using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedStateController : BallStateController {

	BallBehavior ballBehavior;
	Vector2 compressionVector;

	public CompressedStateController (BallBehavior ballBehavior) {
		this.ballBehavior = ballBehavior;
		compressionVector = Vector2.zero;
	}

	public override BallStateController CheckTransitions() {
		if (!ballBehavior.grounded)
			return new AirbornStateController (ballBehavior);
		Vector2 inputDirection = InputUtility.GetInputDirection ();
		Vector2 sumNormal = ballBehavior.GetSumContactNormal ();
		if (Vector2.Dot (inputDirection, sumNormal) >= 0) {
			LaunchBall ();
			return new AirbornStateController (ballBehavior);
		}
		return null;
	}

	public override void Update () {
		Vector2 inputDirection = InputUtility.GetInputDirection ();
		compressionVector += inputDirection * Time.deltaTime * 4;
			if (compressionVector.magnitude > 1)
				compressionVector = compressionVector.normalized;
	}

	public override void Exit () {
		ballBehavior.compressed = false;
	}

	void LaunchBall () {
		Vector2 sumNormal = ballBehavior.GetSumContactNormal ();
		float scaling = Mathf.Abs (Vector2.Dot (sumNormal, compressionVector));
		Vector2 releaseForce = compressionVector * - 200 * scaling;
		ballBehavior.gameObject.GetComponent<Rigidbody2D> ().AddForce (releaseForce);
	}
}
