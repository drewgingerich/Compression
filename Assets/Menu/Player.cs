using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player  {

	public event System.Action<Player> OnLose;

	public int id;
	public Color color;
	ControlScheme controlScheme;
	BallBehavior ball;

	public Player (ControlScheme controlScheme) {
		this.controlScheme = controlScheme;
	}

	// public void LoadBall (BallBehavior ballBehavior) {
	// 	ballBehavior.controlScheme = controlScheme;
	// 	ballBehavior.OnDie += () => { if (OnLose != null) OnLose (this); };
	// 	ballBehavior.LoadPlayer (this);
	// }

	public Vector2 GetInputDirection () {
		return controlScheme.GetInputDirection ();
	}
}
