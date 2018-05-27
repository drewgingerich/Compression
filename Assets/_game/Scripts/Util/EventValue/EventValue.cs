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

#region custom types

[Serializable]
public class PlayerColorSchemeEventValue : EventValue<PlayerColorScheme> { }

[Serializable]
public class InputSchemeEventValue : EventValue<InputScheme> { }

[Serializable]
public class InputTypeEventValue : EventValue<InputType> { }

[Serializable]
public class StateNameEventValue : EventValue<StateName> { }

#endregion

#region builtin types

[Serializable]
public class BoolEventValue : EventValue<bool> { }

[Serializable]
public class IntEventValue : EventValue<int> { }

[Serializable]
public class FloatEventValue : EventValue<float> { }

[Serializable]
public class Vector2EventValue : EventValue<Vector2> { }

#endregion