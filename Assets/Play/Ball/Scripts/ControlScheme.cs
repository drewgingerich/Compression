using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScheme {

	[SerializeField] string horizontalAxisName;
	[SerializeField] string verticalAxisName;

	public Vector2 direction { get; private set; }
	public float magnitude { get; private set; }

	public ControlScheme (string horizontalAxisName, string verticalAxisName) {
		this.horizontalAxisName = horizontalAxisName;
		this.verticalAxisName = verticalAxisName;
	}

	public void UpdateInput() {
		direction = GetInputDirection();
		magnitude = GetInputMagnitude();
	}

	Vector2 GetInputDirection () {
		Vector2 inputVector = new Vector2 (Input.GetAxis (horizontalAxisName), Input.GetAxis (verticalAxisName));
		if (inputVector.magnitude == 0)
			return Vector2.zero;
		else
			return inputVector.normalized;
	}

	float GetInputMagnitude() {
		return 1f;
	}
}
