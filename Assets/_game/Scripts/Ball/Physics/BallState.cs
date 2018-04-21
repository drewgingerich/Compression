using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateName {
	Airborn,
	Grounded,
	Compressed,
	Impact,
	Rebound
}

[System.Serializable]
public class BallState {

	public EventValue<StateName> stateName = new EventValue<StateName>();
	public EventValue<float> timeInState = new EventValue<float>();
	public EventValue<InputType> inputType = new EventValue<InputType>();
	public EventValue<Vector2> inputDirection = new EventValue<Vector2>();
	public EventValue<bool> grounded = new EventValue<bool>();
	public EventValue<float> baseGravity = new EventValue<float>(1f);
	public EventValue<float> gravityRatio = new EventValue<float>(1f);
	public EventValue<Vector2> contactNormal = new EventValue<Vector2>();
	public EventValue<Vector2> reboundDirection = new EventValue<Vector2>();
	public EventValue<Vector2> compressionDirection = new EventValue<Vector2>();
	public EventValue<float> impactMagnitude = new EventValue<float>();
}
