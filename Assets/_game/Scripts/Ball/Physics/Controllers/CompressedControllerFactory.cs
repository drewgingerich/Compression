using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CompressedControllerFactory {

	public static CompressedController GetCompressedController(InputType inputType) {
		switch (inputType) {
			case InputType.Joystick:
			case InputType.Mouse:
			case InputType.Tilt:
			case InputType.Touch:
			case InputType.Keys:
				return new CompressedController();
			default:
				return null;
		}
	}
}
