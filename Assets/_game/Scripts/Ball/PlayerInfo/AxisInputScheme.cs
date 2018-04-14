using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AxisInputScheme", menuName = "Input/AxisInputScheme")]
public class AxisInputScheme : InputScheme {

	[SerializeField] string horizontalAxisName;
	[SerializeField] string verticalAxisName;

	public override Vector2 GetInputDirection() {
		float horizontalInput = Input.GetAxis(horizontalAxisName);
		float verticalInput = Input.GetAxis(verticalAxisName);
		return new Vector2(horizontalInput, verticalInput).normalized;
	}
}
