using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompressedControllerInput {
	public class KeyboardInputHandler : IInputHandler {

		float maxAngularVelocity = 50f;
		float timeCompressed;
		InputScheme inputScheme;
		Vector2 previousInputDirection;

		public KeyboardInputHandler(InputScheme inputScheme) {
			this.inputScheme = inputScheme;
			previousInputDirection = Vector2.zero;
		}

		public Vector2 GetInputVector(Vector2 collisionNormal, float timeCompressed) {
			Vector2 inputDirection = inputScheme.GetInputDirection();
			Vector2 adjustedDirection = AdjustDirection(inputDirection, previousInputDirection);
			previousInputDirection = adjustedDirection;

			// float angularChange = Vector2.Angle(lastInputVector, inputDirection);
			// float angleToMove = Mathf.Min(angularChange, maxAngularMovement);
			// Vector2 orthogonal = Quaternion.AngleAxis(90f, Vector3.forward) * lastInputVector;
			// if (Vector2.Dot(orthogonal, inputDirection) < 0)
			// 	angleToMove *= -1;
			// lastInputVector = Quaternion.AngleAxis(angleToMove, Vector3.forward) * inputDirection;
			// Debug.Log(inputDirection);
			// return lastInputVector;
			return adjustedDirection;
		}

		Vector2 AdjustDirection(Vector2 inputDirection, Vector2 previousInputDirection) {
			if (previousInputDirection == Vector2.zero)
				return inputDirection;
			float angularMovement = Vector2.SignedAngle(previousInputDirection, inputDirection);
			float maxAngularMovement = maxAngularVelocity * Time.deltaTime;
			Debug.Log(maxAngularMovement);
			if (Mathf.Abs(angularMovement) <= maxAngularMovement)
				return inputDirection;
			float clampedMovement = angularMovement >=0 ? maxAngularMovement : -maxAngularMovement;
			Quaternion rotation = Quaternion.AngleAxis(clampedMovement, Vector3.forward);
			return rotation * previousInputDirection;
		}
	}
}