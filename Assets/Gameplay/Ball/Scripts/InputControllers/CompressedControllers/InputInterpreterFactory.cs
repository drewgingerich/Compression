using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompressedControllerInput {

	public static class InputHandlerFactory {

		public static IInputHandler GetInputHandler(InputScheme inputScheme) {
			switch (inputScheme.inputType) {
				case InputScheme.InputType.Keys:
					return new KeyboardInputHandler(inputScheme);
				case InputScheme.InputType.Joystick:
				case InputScheme.InputType.Mouse:
				case InputScheme.InputType.Tilt:
				case InputScheme.InputType.Touch:
				default:
					return null;
			}
		}
	}
}