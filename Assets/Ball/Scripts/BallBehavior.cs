using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour {

	enum State {Airborn, Landing, Grounded, Compressed}
	State state;
	Vector2 compressionForce;
	Vector2 normalVector;
	float baseBounceVelocity = 3;
	bool landing;
	float maxLandingTime = 0.5f;
	float landingTime;

	void Awake () {
		state = State.Airborn;
	}

	Vector2 GetInputForce () {
		Vector2 force = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (force.magnitude > 0)
			force = force / force.magnitude;
		return force;
	}

	void Update () {
		Vector2 force = GetInputForce ();
		if (state == State.Airborn) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (force);
		} else if (state == State.Grounded) {
			if (Vector2.Dot (normalVector, force) < 0)
				state = State.Compressed;
		} else if (state == State.Compressed) {
			if (force == Vector2.zero) {
				Vector2 releaseVelocity = compressionForce * baseBounceVelocity;
				if (landing)
					releaseVelocity = releaseVelocity * 1.5f;
				gameObject.GetComponent<Rigidbody2D> ().velocity = gameObject.GetComponent<Rigidbody2D> ().velocity + releaseVelocity;
				compressionForce = Vector2.zero;
				state = State.Airborn;
			} else {
				compressionForce = force * Vector2.Dot (normalVector, force);
			}
		}
		if (landing) {
			landingTime += Time.deltaTime;
			if (landingTime >= maxLandingTime) {
				landing = false;
				gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0.3f;
				gameObject.GetComponent<Rigidbody2D> ().drag = 0;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collision2D) {
		normalVector = collision2D.contacts[0].normal;
		Debug.Log (Vector2.Dot (normalVector, GetInputForce ()));
		if (Vector2.Dot (normalVector, GetInputForce ()) < 0) {
			landing = true;
			landingTime = 0f;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
			state = State.Compressed;
		} else {
			state = State.Grounded;
		}
	}

	void OnCollisionExit2D (Collision2D collision2D) {
		state = State.Airborn;
		landing = false;
		gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0.3f;
	}

	void OnCollisionStay2D (Collision2D collision2D) {
		normalVector = collision2D.contacts[0].normal;
		if (state == State.Airborn)
			state = State.Grounded;
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
	// 	Vector2 force = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
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