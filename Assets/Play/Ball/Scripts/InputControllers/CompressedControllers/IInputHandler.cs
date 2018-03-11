using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompressedControllerInput {
	public interface IInputHandler {
		Vector2 GetInputVector(Vector2 collisionNormal, float timeCompressed);
	}
}