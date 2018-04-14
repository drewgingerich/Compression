using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BallController {

	public abstract BallController CheckTransitions(BallState state, Rigidbody2D rb2d);
	public virtual void Update(BallState state, Rigidbody2D rb2d) { }
	public virtual void Enter(BallState state, Rigidbody2D rb2d) { }
	public virtual void Exit(BallState state, Rigidbody2D rb2d) { }
}
