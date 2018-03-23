using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnActiveInputEvent : UnityEvent<InputScheme> {}

public class InputActivityDetector : MonoBehaviour {

	public OnActiveInputEvent onActiveInput;
	[SerializeField] List<InputScheme> inputs;

	void Update () {
		int i = 0;
		while (true) {
			if (i >= inputs.Count)
				break;
			InputScheme input = inputs[i];
			Vector2 inputDirection = input.GetInputDirection();
			if (inputDirection != Vector2.zero) {
				onActiveInput.Invoke(input);
				inputs.Remove(input);
			} else {
				i++;
			}
		}
	}
}
