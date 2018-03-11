using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompressedControllerInput {
	public class KeyboardInputHandler : IInputHandler {

		float maxAngularMovement = 10f;
		float timeCompressed;
		InputScheme inputScheme;
		Vector2 lastInputVector;

		public KeyboardInputHandler(InputScheme inputScheme) {
			this.inputScheme = inputScheme;
			lastInputVector = Vector2.zero;
		}

		public Vector2 GetInputVector(Vector2 collisionNormal, float timeCompressed) {
			Vector2 inputDirection = inputScheme.GetInputDirection();
			// float angularChange = Vector2.Angle(lastInputVector, inputDirection);
			// float angleToMove = Mathf.Min(angularChange, maxAngularMovement);
			// Vector2 orthogonal = Quaternion.AngleAxis(90f, Vector3.forward) * lastInputVector;
			// if (Vector2.Dot(orthogonal, inputDirection) < 0)
			// 	angleToMove *= -1;
			// lastInputVector = Quaternion.AngleAxis(angleToMove, Vector3.forward) * inputDirection;
			// Debug.Log(inputDirection);
			// return lastInputVector;
			return inputDirection;
		}
	}
}