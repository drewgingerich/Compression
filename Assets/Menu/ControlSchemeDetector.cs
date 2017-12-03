using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSchemeDetector {

	public event System.Action<ControlScheme> OnDetect;

	List<ControlScheme> schemes;

	public ControlSchemeDetector () {
		schemes = new List<ControlScheme> ();
		schemes.Add (new ControlScheme ("Horizontal", "Vertical"));
		schemes.Add (new ControlScheme ("Horizontal_wasd", "Vertical_wasd"));
	}

	public void CheckForActivity () {
		foreach (ControlScheme controlScheme in schemes) {
			if (controlScheme.GetInputDirection ().magnitude > 0)
				if (OnDetect != null) OnDetect (controlScheme);
		}
	}
}
