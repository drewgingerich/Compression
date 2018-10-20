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

public class BallState {

	public EventValue<StateName> previousState = new EventValue<StateName>();
	public EventValue<StateName> stateName = new EventValue<StateName>();
	public EventValue<float> timeInState = new EventValue<float>();
	public EventValue<int> framesInState = new EventValue<int>();

	public EventValue<InputType> inputType = new EventValue<InputType>();
	public EventValue<Vector2> inputDirection = new EventValue<Vector2>();
	public EventValue<bool> freshInput = new EventValue<bool>();


	public EventValue<bool> grounded = new EventValue<bool>();
	public EventValue<float> timeGrounded = new EventValue<float>();
	public EventValue<float> timeAirborn = new EventValue<float>();

	public EventValue<bool> gravity = new EventValue<bool>(true);
	public EventValue<Vector2> gravityDirection = new EventValue<Vector2>(Vector2.down);
	public EventValue<float> gravityMagnitude = new EventValue<float>(15f);
	public EventValue<float> maxFallSpeed = new EventValue<float>(8f);
	
	public EventValue<bool> sticky = new EventValue<bool>();
	public EventValue<float> stickyMagnitude = new EventValue<float>(5f);

	public EventValue<bool> friction = new EventValue<bool>(true);
	public EventValue<float> frictionMagnitude = new EventValue<float>(5f);

	public EventValue<float> impactMagnitude = new EventValue<float>();
	public EventValue<Vector2> impactDirection = new EventValue<Vector2>();
	public EventValue<Vector2> reboundDirection = new EventValue<Vector2>();
	public EventValue<Vector2> contactNormal = new EventValue<Vector2>();

	public EventValue<Vector2> aimDirection = new EventValue<Vector2>();
	public EventValue<float> aimSpeed = new EventValue<float>(100f);
	public EventValue<float> maxLaunchAngle = new EventValue<float>(65f);
}
