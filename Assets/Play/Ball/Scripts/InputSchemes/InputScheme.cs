using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputScheme {

	public enum InputType { Keys, Joystick, Mouse, Tilt, Touch }

	public readonly InputType inputType;
	public abstract Vector2 GetInputDirection();
}
