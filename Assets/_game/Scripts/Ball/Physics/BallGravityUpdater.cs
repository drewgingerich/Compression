using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGravityUpdater : MonoBehaviour {

	[SerializeField] Ball ball;
	[SerializeField] Rigidbody2D rb2d;

	void OnChangeBaseGravity(float gravity) {
		rb2d.gravityScale = gravity * ball.state.gravityRatio.Value;
	}

	void OnChangeGravityRatio(float gravityRatio) {
		rb2d.gravityScale = ball.state.baseGravity.Value * gravityRatio;
	}

	void OnEnable() {
		ball.state.baseGravity.OnChange += OnChangeBaseGravity;
		ball.state.gravityRatio.OnChange += OnChangeGravityRatio;
	}

	void OnDisable() {
		ball.state.baseGravity.OnChange -= OnChangeBaseGravity;
		ball.state.gravityRatio.OnChange -= OnChangeGravityRatio;
	}
}
