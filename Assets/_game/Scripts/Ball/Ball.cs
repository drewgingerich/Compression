using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public event System.Action<GameObject> OnDie = delegate { };

	public BallState state = new BallState();
	public PlayerInfo playerInfo;

	public void Die() {
		OnDie(gameObject);
		Destroy(gameObject);
	}
}