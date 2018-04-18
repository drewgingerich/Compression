using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallState {

	public EventValue<float> timeInState = new EventValue<float>();
	public EventValue<InputType> inputType = new EventValue<InputType>();
	public EventValue<Vector2> inputDirection = new EventValue<Vector2>();
	public EventValue<bool> grounded = new EventValue<bool>();
	public EventValue<float> baseGravity = new EventValue<float>(1f);
	public EventValue<float> gravityRatio = new EventValue<float>(1f);
	public EventValue<Vector2> contactNormal = new EventValue<Vector2>();
	public EventValue<Vector2> reboundVector = new EventValue<Vector2>();
	public EventValue<Vector2> compressionVector = new EventValue<Vector2>();
	public EventValue<float> impactMagnitude = new EventValue<float>();
	public EventValue<ImpactInfo> impactInfo = new EventValue<ImpactInfo>();
}
