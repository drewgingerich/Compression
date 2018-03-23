using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputScheme : InputScheme {

	public readonly new InputType inputType = InputType.Keys;

	string horizontalAxisName;
	string verticalAxisName;
		
	// float detectionTime = 0f;
	// float magnitudeAcceleration = 3f;
	// float magnitudeLimit = 1;
	// Vector2 cachedInputDirection;

	public KeyboardInputScheme(string horizontalAxisName, string verticalAxisName) {
		this.horizontalAxisName = horizontalAxisName;
		this.verticalAxisName = verticalAxisName;
		inputType = InputType.Keys;
	}

	public override Vector2 GetInputDirection() {
		float horizontalInput = Input.GetAxis(horizontalAxisName);
		float verticalInput = Input.GetAxis(verticalAxisName); 
		return new Vector2 (horizontalInput, verticalInput).normalized;
		// if (inputVector == Vector2.zero) {
		// 	ResetInputDetection();
		// 	return cachedInputDirection;
		// }

		// Vector2 inputDirection = inputVector.normalized;
		// cachedInputDirection = (cachedInputDirection + inputVector).normalized;

		// detectionTime += Time.deltaTime;
		// float magnitude = Mathf.Min (magnitudeLimit, detectionTime * magnitudeAcceleration);

		// return cachedInputDirection * magnitude;
	}

	// public void ResetInputDetection() {
	// 	detectionTime = 0f;
	// 	cachedInputDirection = Vector2.zero;
	// }
}
