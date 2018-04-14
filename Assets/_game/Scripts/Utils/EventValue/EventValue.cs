using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
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
