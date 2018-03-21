using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BallController {

	public abstract BallController CheckTransitions(Ball ball);
	public virtual void Update(Ball ball) { }
	public virtual void Enter(Ball ball) { }
	public virtual void Exit(Ball ball) {}
}
