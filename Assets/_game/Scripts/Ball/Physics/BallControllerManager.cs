using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerManager : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] Rigidbody2D rb2d;

	BallController controller;

	void Awake() {
		controller = new AirbornController();
	}

	void FixedUpdate() {
		ball.state.inputDirection.Value = ball.playerInfo.inputScheme.Value.GetInputDirection();
		bool checkForTransition = true;
		while (checkForTransition) {
			checkForTransition = CheckForNewState();
		}
		controller.Update(ball.state, rb2d);
	}

	bool CheckForNewState() {
		BallController newController = controller.CheckTransitions(ball.state, rb2d);
		if (newController != null) {
			controller.Exit(ball.state, rb2d);
			controller = newController;
			controller.Enter(ball.state, rb2d);
			return true;
		} else {
			return false;
		}
	}
}
