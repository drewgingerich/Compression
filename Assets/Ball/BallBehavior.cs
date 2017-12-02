using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour {

	List<Collision2D> collisions { get; set; }
	BallStateController controller;

	public float speed = 0.5f;
	public bool grounded = false;
	public bool compressed = true;
	public bool sticky = false;
	public float stickyTime = 0f;

	public Vector2 GetSumContactNormal () {
		Vector2 sumNormal = Vector2.zero;
		foreach (Collision2D collision in collisions)
			sumNormal += collision.contacts[0].normal;
		return sumNormal.normalized;
	}

	public void EnableSticky () {

	}

	public void DisableSticky () {

	}

	void Update () {
		BallStateController newController = controller.CheckTransitions ();
		if (newController != null) {
			controller.Exit ();
			controller = newController;
			controller.Enter ();
		}
		controller.Update ();
	}

	void Awake () {
		collisions = new List<Collision2D> ();
		controller = (BallStateController) new AirbornStateController (this);
	}

	void OnCollisionEnter2D (Collision2D collision2D) {
		collisions.Add (collision2D);
		grounded = true;
	}

	void OnCollisionExit2D (Collision2D collision2D) {
		foreach (Collision2D collision in collisions) {
			if (collision.collider == collision2D.collider)
				collisions.Remove (collision);
			break;
		}
		if (collisions.Count == 0)
			grounded = false;
	}
}

	// enum State {Airborn, Landing, Grounded, Compressed}
	// State state;
	// Vector2 compressionForce;
	// Vector2 normalVector;
	// float baseBounceVelocity = 3;
	// bool landing;
	// float maxLandingTime = 0.5f;
	// float landingTime;

	// void Awake () {
	// 	state = State.Airborn;
	// }

	// Vector2 GetInputForce () {
	// 	Vector2 force = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")).normalize;
	// 	if (force.magnitude > 0)
	// 		force = force / force.magnitude;
	// 	return force;
	// }

	// void Update () {
	// 	Vector2 force = GetInputForce ();
	// 	if (state == State.Airborn) {
	// 		gameObject.GetComponent<Rigidbody2D> ().AddForce (force);
	// 	} else if (state == State.Grounded) {
	// 		if (Vector2.Dot (normalVector, force) < 0)
	// 			state = State.Compressed;
	// 	} else if (state == State.Compressed) {
	// 		if (force == Vector2.zero) {
	// 			Vector2 releaseVelocity = compressionForce * baseBounceVelocity;
	// 			if (landing)
	// 				releaseVelocity = releaseVelocity * 1.5f;
	// 			gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity + releaseVelocity;
	// 			compressionForce = Vector2.zero;
	// 			state = State.Airborn;
	// 		} else {
	// 			compressionForce = force * Vector2.Dot (normalVector, force);
	// 		}
	// 	}
	// 	if (landing) {
	// 		landingTime += Time.deltaTime;
	// 		if (landingTime >= maxLandingTime) {
	// 			landing = false;
	// 			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0.3f;
	// 			gameObject.GetComponent<Rigidbody2D> ().drag = 0;
	// 		}
	// 	}
	// }

	// void OnCollisionEnter2D (Collision2D collision2D) {
	// 	normalVector = collision2D.contacts[0].normal;
	// 	Debug.Log (Vector2.Dot (normalVector, GetInputForce ()));
	// 	if (Vector2.Dot (normalVector, GetInputForce ()) < 0) {
	// 		landing = true;
	// 		landingTime = 0f;
	// 		gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
	// 		state = State.Compressed;
	// 	} else {
	// 		state = State.Grounded;
	// 	}
	// }

	// void OnCollisionExit2D (Collision2D collision2D) {
	// 	state = State.Airborn;
	// 	landing = false;
	// 	gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0.3f;
	// }

	// void OnCollisionStay2D (Collision2D collision2D) {
	// 	normalVector = collision2D.contacts[0].normal;
	// 	if (state == State.Airborn)
	// 		state = State.Grounded;
	// }

	/////////////////////////////

		// public enum State { Airborn, Grounded, Compressed };
	// public List<Collision2D> collisions { get; private set; }

	// AirbornState airbornState;
	// GroundedState groundedState;
	// CompressedState compressedState;

	// IBallState stateBehavior;

	// void Awake () {
	// 	airbornState = new AirbornState (this);
	// 	groundedState = new GroundedState (this);
	// 	compressedState = new CompressedState (this);
	// 	stateBehavior = airbornState;
	// 	stateBehavior.Enter ();
	// 	collisions = new List<Collision2D> ();
	// }

	// void Update () {
	// 	stateBehavior.Update ();
	// }

	// public void SetState (State state) {
	// 	stateBehavior.Exit ();
	// 	if (state == State.Airborn)
	// 		stateBehavior = airbornState;
	// 	stateBehavior.Enter ();
	// }

	// void OnCollisionEnter2D (Collision2D collision2D) {
	// 	collisions.Add (collision2D);
	// }

	// void OnCollisionExit2D (Collision2D collision2D) {
	// 	collisions.Remove (collision2D);
	// }