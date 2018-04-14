using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InputType { Keys, Joystick, Mouse, Tilt, Touch }

public abstract class InputScheme : ScriptableObject {

	public InputType inputType;

	public abstract Vector2 GetInputDirection();
}
