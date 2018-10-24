using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class SimpleBallController : MonoBehaviour {

	[SerializeField]
	private float baseSpeed = 1f;

	private Actor actor;

	private Vector2 velocity;
	private Vector2 move;


	void Awake() {
		actor = GetComponent<Actor>();
	}

	void FixedUpdate () {
		velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
		move = velocity * baseSpeed * Time.fixedDeltaTime;
		if (move != Vector2.zero) {
			actor.Move(move, RealignMovement);
		}
	}

	void RealignMovement(RaycastHit2D hit) {
		Vector2 remainingMove = move * (1 - hit.fraction);
		Debug.Log(remainingMove);
		Vector2 surfaceVector = hit.normal.Perpendicular();
		Vector2 surfaceVelocity = surfaceVector * Vector2.Dot(remainingMove, surfaceVector);
		actor.Move(surfaceVelocity, null);
	}
}
