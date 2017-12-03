using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScheme {

	string hAxisName;
	string vAxisName;

	public ControlScheme (string hAxisName, string vAxisName) {
		this.hAxisName = hAxisName;
		this.vAxisName = vAxisName;
	}

	public Vector2 GetInputDirection () {
		Vector2 inputVector = new Vector2 (Input.GetAxis (hAxisName), Input.GetAxis (vAxisName));
		if (inputVector.magnitude == 0)
			return Vector2.zero;
		else
			return inputVector.normalized;
	}
}
