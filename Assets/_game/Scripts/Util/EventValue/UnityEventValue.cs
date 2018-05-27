using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventValue <T> {

	[SerializeField] T _value;

	protected virtual void CallEvent (T value) {}

	public T Value {
		get { return _value; }
		set {
			_value = value;
			CallEvent(value);
		}
	}

	public UnityEventValue() { }

	public UnityEventValue(T initialValue) {
		_value = initialValue;
	}
}

[Serializable]
public class EventableInt : UnityEventValue<int> {
	public IntEvent OnChanged;
	protected override void CallEvent(int value) { OnChanged.Invoke(value); }
}

[Serializable]
public class EventableFloat : UnityEventValue<float> {
	public FloatEvent OnChanged;
	protected override void CallEvent(float value) {OnChanged.Invoke(value);}
}

[Serializable]
public class EventableBool : UnityEventValue<bool> {
	public BoolEvent OnChanged;
	protected override void CallEvent(bool value) { OnChanged.Invoke(value); }
}

[Serializable]
public class EventableVector2 : UnityEventValue<Vector2> {
	public Vector2Event OnChanged;
	protected override void CallEvent(Vector2 value) { OnChanged.Invoke(value); }
}

[Serializable]
public class EventableInputType : UnityEventValue<InputType> {
	public InputTypeEvent OnChanged;
	protected override void CallEvent(InputType value) { OnChanged.Invoke(value); }
}

[Serializable]
public class EventableInputScheme : UnityEventValue<InputScheme> {
	public InputSchemeEvent OnChanged;
	protected override void CallEvent(InputScheme value) { OnChanged.Invoke(value); }
}

[Serializable]
public class EventablePlayerColor: UnityEventValue<PlayerColorScheme> {
	public PlayerColorEvent OnChanged;
	protected override void CallEvent(PlayerColorScheme value) { OnChanged.Invoke(value); }
}
