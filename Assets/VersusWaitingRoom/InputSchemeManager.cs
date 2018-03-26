using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSchemeManager : MonoBehaviour {

	[SerializeField] List<InputScheme> inputs;

	public InputSchemeEvent OnActiveInputScheme;

	void Update () {
		int i = 0;
		while (true) {
			if (i >= inputs.Count)
				break;
			InputScheme input = inputs[i];
			Vector2 inputDirection = input.GetInputDirection();
			if (inputDirection != Vector2.zero) {
				OnActiveInputScheme.Invoke(input);
				DergisterInputScheme(input);
			} else {
				i++;
			}
		}
	}
	
	public void RegisterInputScheme(InputScheme inputScheme) {
		inputs.Add(inputScheme);
	}

	public void DergisterInputScheme(InputScheme inputScheme) {
		inputs.Remove(inputScheme);
	}
}
