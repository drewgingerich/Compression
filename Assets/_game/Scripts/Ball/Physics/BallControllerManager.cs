using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerManager : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] Rigidbody2D rb2d;

	BallController controller;

	void Awake() {
		controller = new AirbornController();
		controller.Enter(ball.state, rb2d);
	}

	void FixedUpdate() {
		ball.state.inputDirection.Value = ball.playerInfo.inputScheme.Value.GetInputDirection();
		if (ball.state.inputDirection.Value == Vector2.zero) {
			ball.state.freshInput.Value = true;
		}
		bool checkForTransition = true;
		while (checkForTransition) {
			checkForTransition = CheckForNewState();
		}
		controller.Update(ball.state, rb2d);
		if (ball.state.grounded.Value)
			ball.state.timeGrounded.Value += Time.fixedDeltaTime;
		else
			ball.state.timeAirborn.Value += Time.fixedDeltaTime;
		ball.state.timeInState.Value += Time.fixedDeltaTime;
		ball.state.framesInState.Value += 1;
	}

	bool CheckForNewState() {
		BallController newController = controller.CheckTransitions(ball.state, rb2d);
		if (newController != null) {
			controller.Exit(ball.state, rb2d);
			controller = newController;
			ball.state.timeInState.Value = 0f;
			ball.state.framesInState.Value = 0;
			controller.Enter(ball.state, rb2d);
			return true;
		} else {
			return false;
		}
	}
}
