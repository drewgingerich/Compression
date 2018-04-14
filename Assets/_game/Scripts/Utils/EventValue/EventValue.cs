using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventValue<T> {

	public event System.Action<T> OnChange;

	[SerializeField] T _value;

	public T Value {
		get { return _value; }
		set {
			_value = value;
			if (OnChange != null)
				OnChange(value);
		}
	}

	public EventValue() { }

	public EventValue(T initialValue) {
		_value = initialValue;
	}
}

[System.Serializable]
public class BoolEventValue : EventValue<bool> { }

[System.Serializable]
public class IntEventValue : EventValue<int> { }

[System.Serializable]
public class FloatEventValue : EventValue<float> { }

[System.Serializable]
public class PlayerColorSchemeEventValue : EventValue<PlayerColorScheme> { }

[System.Serializable]
public class InputSchemeEventValue : EventValue<InputScheme> { }