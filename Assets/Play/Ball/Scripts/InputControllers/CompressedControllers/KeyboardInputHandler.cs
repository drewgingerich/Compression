using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompressedControllerInput {
	public class KeyboardInputHandler : IInputHandler {

		float maxAngle = 20f;
		float maxAngularVelocity = 50f;
		float timeCompressed;
		InputScheme inputScheme;
		Vector2 previousInputDirection;

		public KeyboardInputHandler(InputScheme inputScheme) {
			this.inputScheme = inputScheme;
			previousInputDirection = Vector2.zero;
		}

		public Vector2 GetInputVector(Vector2 reflectionDirection, float timeCompressed) {
			Vector2 inputDirection = inputScheme.GetInputDirection();
			Vector2 clampedDirection = ClampDirection(inputDirection, -reflectionDirection, maxAngle);
			Debug.Log(clampedDirection);
			Vector2 smoothedDirection = SmoothDirectionChange(clampedDirection, previousInputDirection, maxAngularVelocity);
			previousInputDirection = smoothedDirection;
			return smoothedDirection;
		}

		Vector2 ClampDirection(Vector2 direction, Vector2 referenceDirection, float maxAngle) {
			float angle = Vector2.SignedAngle(referenceDirection, direction);
			if (Mathf.Abs(angle) <= maxAngle)
				return direction;
			float clampedAngle = angle >= 0 ? maxAngle : -maxAngle;
			Quaternion rotation = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
			return rotation * referenceDirection;
	}
		
		Vector2 SmoothDirectionChange(Vector2 direction, Vector2 referenceDirection, float maxAngularVelocity) {
			if (referenceDirection == Vector2.zero)
				return direction;
			float angularMovement = Vector2.SignedAngle(referenceDirection, direction);
			float maxAngularMovement = maxAngularVelocity * Time.deltaTime;
			if (Mathf.Abs(angularMovement) <= maxAngularMovement)
				return direction;
			float smoothedMovement = angularMovement >=0 ? maxAngularMovement : -maxAngularMovement;
			Quaternion rotation = Quaternion.AngleAxis(smoothedMovement, Vector3.forward);
			return rotation * referenceDirection;
		}

		float FindMagnitude() {
			return 1f;
		}
	}
}