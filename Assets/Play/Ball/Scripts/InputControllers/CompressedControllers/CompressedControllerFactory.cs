using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CompressedControllerFactory {

	public static CompressedController GetCompressedController(InputScheme inputScheme) {
		switch (inputScheme.inputType) {
			case InputScheme.InputType.Joystick:
			case InputScheme.InputType.Mouse:
			case InputScheme.InputType.Tilt:
			case InputScheme.InputType.Touch:
			case InputScheme.InputType.Keys:
				return new KeyboardCompressedController();
			default:
				return null;
		}
	}
}
