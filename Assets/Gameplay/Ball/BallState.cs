using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState {

	public event System.Action<float> OnChangeBallMass = delegate { };
	public event System.Action<float> OnChangeBaseGravity = delegate { };
	public event System.Action<float> OnChangeCurrentGravity = delegate { };
	public event System.Action<Vector2> OnChangeReboundDirection = delegate { };
	public event System.Action<Vector2> OnChangeContactNormal = delegate { };
	public event System.Action<float> OnChangeImpactMagnitude = delegate { };

	float ballMass = 1f;
	float baseGravity = 1f;
	float currentGravity = 1f;
	Vector2 contactNormal;
	Vector2 reboundDirection;
	float impactMagnitude = 0f;
	bool grounded;
	Ball ball;

	public float BallMass {
		get { return ballMass; }
		set {
			ballMass = value;
			OnChangeBallMass(value);
		}
	}
	public float BaseGravity {
		get { return baseGravity; }
		set {
			baseGravity = value;
			OnChangeBaseGravity(value);
		}
	}
	public float CurrentGravity {
		get { return currentGravity; }
		set { 
			currentGravity = value;
			ball.GetComponent<Rigidbody2D>().gravityScale = currentGravity;
			OnChangeCurrentGravity(value);
		}
	}
	public Vector2 ContactNormal {
		get { return contactNormal; }
		set {
			contactNormal = value;
			OnChangeContactNormal(value);
		}
	}
	public Vector2 ReboundDirection {
		get { return reboundDirection; }
		set {
			reboundDirection = value;
			OnChangeReboundDirection (value);
		}
	}
	public float ImpactMagnitude {
		get { return impactMagnitude; }
		set {
			impactMagnitude = value;
			OnChangeImpactMagnitude(value);
		}
	}
	public bool Grounded {
		get { return grounded; }
		set { grounded = value; }
	}

	public BallState(Ball ball) {
		this.ball = ball;
	}
}
