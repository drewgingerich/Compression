using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateName {
	Airborn,
	Airjump,
	Grounded,
	Compressed,
	Impact,
	Rebound
}

[System.Serializable]
public class BallState {

	public EventValue<StateName> previousState = new EventValue<StateName>();
	public EventValue<StateName> stateName = new EventValue<StateName>();
	public EventValue<float> timeInState = new EventValue<float>();

	public EventValue<InputType> inputType = new EventValue<InputType>();
	public EventValue<Vector2> inputDirection = new EventValue<Vector2>();
	public EventValue<bool> freshInput = new EventValue<bool>();

	public EventValue<bool> grounded = new EventValue<bool>();
	public EventValue<float> timeGrounded = new EventValue<float>();
	public EventValue<float> timeAirborn = new EventValue<float>();
	public EventValue<bool> airjumpAvailable = new EventValue<bool>();

	public EventValue<float> baseGravity = new EventValue<float>(1f);
	public EventValue<float> gravityRatio = new EventValue<float>(1f);

	public EventValue<float> impactMagnitude = new EventValue<float>();
	public EventValue<Vector2> reboundDirection = new EventValue<Vector2>();
	public EventValue<Vector2> contactNormal = new EventValue<Vector2>();

	public EventValue<Vector2> compressionDirection = new EventValue<Vector2>();
}
