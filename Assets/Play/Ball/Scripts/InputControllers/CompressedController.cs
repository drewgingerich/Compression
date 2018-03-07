using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressedController : BallController {

	Ball ball;
	Vector2 releaseVector;
	// float maxStickyTime = 0.5f;

	public CompressedController(Ball ball) {
		this.ball = ball;
	}

	public override BallController CheckTransitions() {
		if (!ball.state.grounded) {
			return new AirbornController(ball);
		}
		Vector2 inputDirection = ball.controlScheme.direction;
		Vector2 sumNormal = ball.collisionManager.GetSumContactNormal();
		if (Vector2.Dot(inputDirection, sumNormal) >= 0) {;
			return new AirbornController(ball);
		}
		return null;
	}

	public override void Update() {
		Vector2 inputDirection = ball.controlScheme.direction;
		if (inputDirection.magnitude > releaseVector.magnitude)
			releaseVector = inputDirection;
		// float inputMagnitude = ball.controlScheme.magnitude;
		// ball.aimBar.UpdateAimBar (compressionVector, ball.GetSumContactNormal ());
		// ball.ballSprite.RotateToNormal (ball.GetSumContactNormal ());
		// if (ball.sticky) {
		// 	ball.stickyTime += Time.deltaTime;
		// 	if (ball.stickyTime >= maxStickyTime)
		// 		ball.DisableSticky ();
		// }
	}

	public override void Enter() {
		// ball.aimBar.ShowBar ();
		// if (!ball.sticky)
		// 	ball.EnableSticky ();
		// ball.animator.SetTrigger ("squish");
	}

	public override void Exit() {
		Vector2 sumNormal = ball.collisionManager.GetSumContactNormal();
		LaunchBall(releaseVector, sumNormal);
		// ball.aimBar.HideBar ();
		// if (ball.sticky)
		// 	ball.DisableSticky ();
		// ball.animator.SetTrigger ("launch");
	}

	void LaunchBall(Vector2 releaseVector, Vector2 sumNormal) {
		float compressionScaling = Mathf.Sqrt(Mathf.Abs(Vector2.Dot(sumNormal, releaseVector)));
		// float stickyScaling = ball.sticky ? 250 : 150;
		float stickyScaling = 250;
		Vector2 releaseForce = releaseVector * -stickyScaling * compressionScaling;
		ball.gameObject.GetComponent<Rigidbody2D>().AddForce(releaseForce);
	}
}
