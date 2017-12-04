using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallStateController {

	public abstract BallStateController CheckTransitions ();
	public abstract void Update ();
	public virtual void Enter () {}
	public virtual void Exit () {}
}
