using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedStateController : BallStateController {

	BallBehavior ballBehavior;
	Vector2 compressionVector;
	float compressionVectorChangeSpeed = 6;
	float maxStickyTime = 0.4f;

	public CompressedStateController (BallBehavior ballBehavior) {
		this.ballBehavior = ballBehavior;
		compressionVector = Vector2.zero;
	}

	public override BallStateController CheckTransitions() {
		if (!ballBehavior.grounded) {
			return new AirbornStateController (ballBehavior);
		}
		Vector2 inputDirection = InputUtility.GetInputDirection ();
		Vector2 sumNormal = ballBehavior.GetSumContactNormal ();
		if (Vector2.Dot (inputDirection, sumNormal) >= 0) {
			LaunchBall ();
			return new AirbornStateController (ballBehavior);
		}
		return null;
	}

	public override void Enter () {
		ballBehavior.aimBar.ShowBar ();
		if (!ballBehavior.sticky)
			ballBehavior.EnableSticky ();
		ballBehavior.animator.SetTrigger ("squish");
	}

	public override void Exit () {
		ballBehavior.aimBar.HideBar ();
		if (ballBehavior.sticky)
			ballBehavior.DisableSticky ();
		ballBehavior.animator.SetTrigger ("launch");
	}

	public override void Update () {
		Vector2 inputDirection = InputUtility.GetInputDirection ();
		compressionVector += inputDirection * Time.deltaTime * compressionVectorChangeSpeed;
		if (compressionVector.magnitude > 1)
			compressionVector = compressionVector.normalized;
		ballBehavior.aimBar.UpdateAimBar (compressionVector, ballBehavior.GetSumContactNormal ());
		ballBehavior.ballSprite.RotateToNormal (ballBehavior.GetSumContactNormal ());
		if (ballBehavior.sticky) {
			ballBehavior.stickyTime += Time.deltaTime / 2;
			if (ballBehavior.stickyTime >= maxStickyTime)
				ballBehavior.DisableSticky ();
		}
	}

	void LaunchBall () {
		Vector2 sumNormal = ballBehavior.GetSumContactNormal ();
		float scaling = Mathf.Sqrt (Mathf.Abs (Vector2.Dot (sumNormal, compressionVector)));
		Vector2 releaseForce = compressionVector * - 200 * scaling;
		ballBehavior.gameObject.GetComponent<Rigidbody2D> ().AddForce (releaseForce);
	}
}
