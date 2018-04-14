using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallState {

	public EventValue<float> timeInState;
	public EventValue<InputType> inputType;
	public EventValue<Vector2> inputDirection;
	public EventValue<bool> grounded;
	public EventValue<float> baseGravity;
	public EventValue<float> currentGravity;
	public EventValue<Vector2> contactNormal;
	public EventValue<ImpactInfo> impactInfo;
}
